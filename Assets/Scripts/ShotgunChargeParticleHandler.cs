using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunChargeParticleHandler : MonoBehaviour
{
    float startTime, currentTime;
    
    ParticleSystem particleSyst;
    Shotgun shotgun;

    private void OnEnable()
    {
        shotgun = transform.root.GetComponentInChildren<Shotgun>();
        shotgun.OnItemUsed += DestroySelf;
    }

    private void OnDisable()
    {
        shotgun.OnItemUsed -= DestroySelf;
    }

    private void Awake()
    {
        startTime = Time.time;
        currentTime = startTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        particleSyst = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= startTime + particleSyst.main.duration - Mathf.Epsilon)
        {
            particleSyst.Pause();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    void DestroySelf(float uselessNum)
    {
        Destroy(gameObject);
    }
}
