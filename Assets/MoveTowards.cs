using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] float moveTime;
    [SerializeField] bool parentToDestination = false;

    Vector3 initialPos;

    private void Awake()
    {
        initialPos = transform.position;
    }

    private void Start()
    {
        if (parentToDestination)
            transform.parent = destination;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < moveTime)
        {
            transform.position = Vector3.Lerp(initialPos, destination.position, Time.time/moveTime);
        }
        else
        {
            transform.position = destination.position;
        }
    }
}
