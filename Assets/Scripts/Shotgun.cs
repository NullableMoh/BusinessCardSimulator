using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shotgun : UsableItem
{
    [SerializeField] Transform muzzleFlashTransform, chargingParticlesTransform; 
    [SerializeField] GameObject bulletHitParticleEffect, muzzleFlash, shotgunChargingParticleEffect;
    [SerializeField] AudioClip gunshotSound, shotgunChargeSound;
    [SerializeField] float recoilFactor;

    float chargeSoundPlayStartTime;
    bool shotgunCharged;

    AudioSource audioSource;
    PlayerInputActions inputActions;

    public override event Action<float> OnItemUsed;
    public event Action OnShotgunCharging;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();

        shotgunCharged = false;
    }

    private void OnEnable()
    {
        inputActions.Player.UseUsableItem.performed += UseItem;
        inputActions.Player.UsableItemOptional2ndAbility.performed += ChargeAbility;
    }

    private void OnDisable()
    {
        inputActions.Player.UseUsableItem.performed -= UseItem;
        inputActions.Player.UsableItemOptional2ndAbility.performed -= ChargeAbility;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChargeAbility(InputAction.CallbackContext callbackContext)
    {
        if (GetComponentInChildren<ShotgunChargeParticleHandler>()) return;

        if (transform.parent == null) return;

        var particle = Instantiate(shotgunChargingParticleEffect, chargingParticlesTransform.position, Quaternion.identity, chargingParticlesTransform);

        OnShotgunCharging?.Invoke();
        audioSource.PlayOneShot(shotgunChargeSound);
        chargeSoundPlayStartTime = Time.time;

        shotgunCharged = true;
    }

    public override void UseItem(InputAction.CallbackContext callbackContext)
    {
        if (transform.parent == null) return;
        if (Time.time < chargeSoundPlayStartTime + shotgunChargeSound.length) return;
        if (shotgunCharged == false) return;

        bool raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, Mathf.Infinity, ~LayerMask.GetMask(Strings.Layers.Player, Strings.Layers.PlayerProjectile, Strings.Layers.UsableItem, Strings.Layers.UsableItemHolder, Strings.Layers.Pickups));
        
        if (raycastHit)
        {
            audioSource.PlayOneShot(gunshotSound);
            OnItemUsed?.Invoke(recoilFactor);
            shotgunCharged = false;

            Instantiate(bulletHitParticleEffect, hit.point, Quaternion.LookRotation(-Camera.main.transform.forward));
            Instantiate(muzzleFlash, muzzleFlashTransform.position, Quaternion.LookRotation(Camera.main.transform.forward), transform);
        }
    }
}
