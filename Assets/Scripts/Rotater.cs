using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [SerializeField] Vector3 rotationFactor;

    void FixedUpdate()
    {
        transform.Rotate(rotationFactor * Time.fixedDeltaTime);      
    }
}
