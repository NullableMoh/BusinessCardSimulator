using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] GameObject keyVFX, damageParticleSystem;

    public event Action OnKeyCreated;

    StoneLeg stoneLeg;

    private void OnEnable()
    {
        stoneLeg = FindObjectOfType<StoneLeg>();
        
        if(stoneLeg)
            stoneLeg.OnStoneLegKicked += TryTakeDamageFromLeg;
    }

    private void OnDisable()
    {
        if(stoneLeg)
            stoneLeg.OnStoneLegKicked -= TryTakeDamageFromLeg;
    }

    void TryTakeDamageFromLeg(GameObject obj)
    {
        if (obj != gameObject) return;

        health -= 3;

        Instantiate(damageParticleSystem, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);

        if (health <= 0)
        {
            Instantiate(keyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var projectile = other.gameObject.GetComponent<PlayerProjectile>();
        if(projectile)
        {
            health--;

            Instantiate(damageParticleSystem, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);

            if (health <= 0)
            {
                Instantiate(keyVFX, transform.position, Quaternion.identity);
                OnKeyCreated?.Invoke();
                Destroy(gameObject);
            }

        }
    }
}
