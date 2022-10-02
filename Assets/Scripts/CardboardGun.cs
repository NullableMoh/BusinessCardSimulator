using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardboardGun : UsableItem
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float projectileSpeed;

    public override void UseItem()
    {
        GameObject bullet = Instantiate(projectile, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * projectileSpeed * Time.deltaTime;
    }
}
