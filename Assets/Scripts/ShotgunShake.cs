using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShake : MonoBehaviour
{
    [SerializeField] float shakeSpeed, shakeMultiplier;

    bool startShake;
    Vector3 startShakeInitialLocalPos;

    Shotgun shotgun;

    private void OnEnable()
    {
        shotgun = GetComponent<Shotgun>();
        shotgun.OnShotgunCharging += StartShake;
        shotgun.OnItemUsed += StopShake;
    }
    
    void OnDisable()
    {
        shotgun.OnShotgunCharging -= StartShake;
        shotgun.OnItemUsed -= StopShake;
    }

    private void StartShake()
    {
        startShake = true;
        startShakeInitialLocalPos = transform.localPosition;
    }

    void StopShake(float uselessNum)
    {
        startShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startShake)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + Mathf.Sin(Time.time * shakeSpeed) * shakeMultiplier, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
