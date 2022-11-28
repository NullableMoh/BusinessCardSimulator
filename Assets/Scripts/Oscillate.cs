using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] float xOscillation, yOscillation, zOscillation;

    Vector3 initalPos;

    private void Awake() => initalPos = transform.position;

    // Update is called once per frame
    void Update() => transform.position = new Vector3(
            initalPos.x + Mathf.Sin(Time.time) * xOscillation,
            initalPos.y + Mathf.Sin(Time.time) * yOscillation,
            initalPos.z + Mathf.Sin(Time.time) * zOscillation
            );
}
