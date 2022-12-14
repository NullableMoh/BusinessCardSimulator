using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoneLeg : UsableItem
{
    [SerializeField] float hitRange;
    [SerializeField] AudioClip hitAudio;
    [SerializeField] GameObject hitParticles;
    [SerializeField] Transform hitParticleSpawnPoint;

    public override event Action<float> OnItemUsed;
    public event Action OnStoneLegKickStarted;
    public event Action<GameObject> OnStoneLegKicked;

    bool kickDone;
    string currentAnimState;

    Animator anim;
    StoneLegAnimatorEvents stoneLegAnimEvents;
    AudioSource audioSource;
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();

        kickDone = true;
    }

    private void OnEnable()
    {
        stoneLegAnimEvents = GetComponentInChildren<StoneLegAnimatorEvents>();
        stoneLegAnimEvents.OnKickAnimFinished += OnKickAnimDone;
        inputActions.Player.UseUsableItem.performed += UseItem;
    }

    private void OnDisable()
    {
        stoneLegAnimEvents.OnKickAnimFinished -= OnKickAnimDone;
        inputActions.Player.UseUsableItem.performed -= UseItem;
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override void UseItem(InputAction.CallbackContext callbackContext)
    {
        if (transform.parent == null || !kickDone) return;

        PlayAnimation(Strings.Animations.UsableItems.StoneLeg.Kick);
        OnStoneLegKickStarted?.Invoke();
        kickDone = false;
    }

    void PlayAnimation(string newState)
    {
        if (currentAnimState == newState) return;

        currentAnimState = newState;
        anim.Play(currentAnimState);
    }

    private void OnKickAnimDone()
    {
        kickDone = true;
        
        PlayAnimation(Strings.Animations.UsableItems.StoneLeg.Idle);

        var raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, hitRange, ~LayerMask.GetMask(Strings.Layers.Player, Strings.Layers.PlayerProjectile, Strings.Layers.UsableItem, Strings.Layers.UsableItemHolder, Strings.Layers.Pickups));
        if(raycastHit)
        {
            OnStoneLegKicked?.Invoke(hit.transform.gameObject);
            audioSource.PlayOneShot(hitAudio);
            Instantiate(hitParticles, hitParticleSpawnPoint.position, Quaternion.LookRotation(-Camera.main.transform.forward));
        }

        OnItemUsed?.Invoke(5f);
    }
}
