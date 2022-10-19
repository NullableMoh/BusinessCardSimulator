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

    //you need to turn audio into its own script
    //you need to put ALL camera movement things into one script (mouseLook, rename it maybe too)
    public override event Action<float> OnUse;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void UseItem()
    {
        var particle = Instantiate(bulletParticle, bulletSpawnTransform.position, Quaternion.identity, bulletSpawnTransform);

        bool raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, LayerMask.GetMask("Ground"));
        if (raycastHit)
        {
            particle.HitPoint = hit.point;
        }
        else
        {
            particle.Direction = transform.forward;
        }

        audioSource.PlayOneShot(gunshotSound);

        OnUse?.Invoke(recoilFactor);
    }
}
