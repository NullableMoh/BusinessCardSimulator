using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardboardGun : UsableItem
{
    [SerializeField] Transform muzzleFlashPosition;
    [SerializeField] GameObject projectileHitParticle;
    [SerializeField] GameObject muzzleFlashParticle;
    [SerializeField] GameObject bulletSpawnPoint;
    [SerializeField] float projectileSpeed;

    public override void UseItem()
    {
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit))
        {
            Instantiate(muzzleFlashParticle, muzzleFlashPosition.position, Quaternion.LookRotation(Camera.main.transform.forward)s);
            Instantiate(projectileHitParticle, hit.point, Quaternion.identity);
        }

    }
}
