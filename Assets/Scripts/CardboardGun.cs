using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardboardGun : UsableItem
{
    [SerializeField] Transform bulletSpawnTransform;
    [SerializeField] CardboardGunBullet bulletParticle;

    public override void UseItem()
    {
        var particle = Instantiate(bulletParticle, bulletSpawnTransform.position, Quaternion.identity, bulletSpawnTransform);
        
        bool raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, LayerMask.GetMask("Ground"));
        if(raycastHit)
        {
            particle.HitPoint = hit.point;
        }
        else
        {
            particle.Direction = transform.forward;
        }
    }
}
