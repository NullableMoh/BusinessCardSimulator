using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardGunProjectile : MonoBehaviour
{
    [SerializeField] GameObject hitParticles;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitParticles);
        Destroy(gameObject);
    }
}
