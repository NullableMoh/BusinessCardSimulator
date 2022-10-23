using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardboardGun : UsableItem
{
    [SerializeField] Transform bulletSpawnTransform;
    [SerializeField] CardboardGunBullet bulletParticle;
    [SerializeField] AudioClip gunshotSound;
    [SerializeField] float recoilFactor;

    AudioSource audioSource;
    PlayerInputActions inputActions;

    public override event Action<float> OnItemUsed;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }

    private void OnEnable()
    {
        inputActions.Player.UseUsableItem.performed += UseItem;
    }

    private void OnDisable()
    {
        inputActions.Player.UseUsableItem.performed -= UseItem;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void UseItem(InputAction.CallbackContext callbackContext)
    {
        if (transform.parent == null) return;

        var particle = Instantiate(bulletParticle, bulletSpawnTransform.position, Quaternion.identity, bulletSpawnTransform);
        
        bool raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, Mathf.Infinity, ~LayerMask.GetMask(Strings.Layers.Player, Strings.Layers.PlayerProjectile, Strings.Layers.UsableItem, Strings.Layers.UsableItemHolder, Strings.Layers.Pickups));
        particle.CalculateDirection(hit, raycastHit, this);
        
        audioSource.PlayOneShot(gunshotSound);
        OnItemUsed?.Invoke(recoilFactor);
    }

}
