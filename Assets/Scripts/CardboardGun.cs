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

    public override event Action<float> OnUse;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void UseItem()
    {
        var particle = Instantiate(bulletParticle, bulletSpawnTransform.position, Quaternion.identity, bulletSpawnTransform);
        
        bool raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, Mathf.Infinity, ~LayerMask.GetMask(Strings.Layers.Player, Strings.Layers.PlayerProjectile, Strings.Layers.UsableItem, Strings.Layers.UsableItemHolder, Strings.Layers.Pickups));
        particle.CalculateDirection(hit, raycastHit, this);
        
        audioSource.PlayOneShot(gunshotSound);
        OnUse?.Invoke(recoilFactor);
    }
}
