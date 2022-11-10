using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] Material[] breakMaterialStages;
    [SerializeField] AudioClip damageSound, breakSound;
    [SerializeField] GameObject damageParticleSystem;

    int materialStage;

    Shotgun shotgun;

    MeshRenderer meshRend;
    Collider col;
    AudioSource audioSource;

    private void Awake()
    {
        materialStage = 0;
    }

    private void OnEnable()
    {
        shotgun = FindObjectOfType<Shotgun>();
        shotgun.OnShotgunFired += TryTakeDamage;
    }


    private void OnDisable()
    {
        shotgun.OnShotgunFired -= TryTakeDamage;
    }

    // Start is called before the first frame update
    void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerProjectile>()) return;

        if (materialStage < breakMaterialStages.Length)
        {
            meshRend.material.mainTexture = breakMaterialStages[materialStage].mainTexture;
            materialStage++;
            audioSource.PlayOneShot(damageSound);
            Instantiate(damageParticleSystem);
        }
        else
            DestroyByFinalDamage();
    }

    private void TryTakeDamage(GameObject obj)
    {
        if (obj != gameObject) return;

        if (materialStage < breakMaterialStages.Length)
        {
            materialStage = breakMaterialStages.Length - 1;
            meshRend.material.mainTexture = breakMaterialStages[materialStage].mainTexture;
            audioSource.PlayOneShot(damageSound);
            Instantiate(damageParticleSystem);

            materialStage++;
        }
        else
            DestroyByFinalDamage();
    }

    void DestroyByFinalDamage()
    {
        audioSource.PlayOneShot(breakSound);
        col.enabled = false;
        meshRend.enabled = false;
        Instantiate(damageParticleSystem);
        Destroy(gameObject, breakSound.length);
    }


}
