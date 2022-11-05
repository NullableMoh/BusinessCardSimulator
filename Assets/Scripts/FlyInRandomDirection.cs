using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyInRandomDirection : MonoBehaviour
{
    [SerializeField] Vector3 randomDir;
    [SerializeField] float speed;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = randomDir.normalized * speed * Time.deltaTime;
    }
}
