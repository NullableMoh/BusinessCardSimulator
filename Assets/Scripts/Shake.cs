using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] float shakeSpeed, shakeMultiplier;

    Vector3 initialLocalPos;

    private void Awake()
    {
        initialLocalPos = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(Mathf.Sin(Time.time * shakeSpeed) * shakeMultiplier + initialLocalPos.x, transform.localPosition.y, Mathf.Sin(Time.time * shakeSpeed) * shakeMultiplier + initialLocalPos.z);
    }
}
