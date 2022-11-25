using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackStoneBreakable : MonoBehaviour
{
    [SerializeField] Material[] breakMaterialStages;
    [SerializeField] AudioClip damageSound, breakSound;
    [SerializeField] GameObject damageParticleSystem;
    [SerializeField] int shotgunDamage;

    int materialStage;

    Shotgun shotgun;

    MeshRenderer meshRend;
    Collider col;
    AudioSource audioSource;

    private void Awake()
    {
        materialStage = -1;
    }

    private void OnEnable()
    {
        shotgun = FindObjectOfType<Shotgun>();
        if (shotgun)
        {
            shotgun.OnShotgunFired += TryTakeDamageFromShotgun;
        }
    }

    private void OnDisable()
    {
        if (shotgun)
        {
            shotgun.OnShotgunFired -= TryTakeDamageFromShotgun;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void TryTakeDamageFromShotgun(GameObject obj)
    {
        if (obj != gameObject) return;
        
        materialStage += shotgunDamage;

        if (materialStage < breakMaterialStages.Length)
        {
            meshRend.material.mainTexture = breakMaterialStages[materialStage].mainTexture;
            audioSource.PlayOneShot(damageSound);
            Instantiate(damageParticleSystem, transform.position, Quaternion.identity);
        }
        else
            DestroyByFinalDamage();

    }

    void DestroyByFinalDamage()
    {
        audioSource.PlayOneShot(breakSound);
        col.enabled = false;
        meshRend.enabled = false;
        Instantiate(damageParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject, breakSound.length);
    }


}
