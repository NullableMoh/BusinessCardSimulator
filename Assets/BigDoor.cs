using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
    [SerializeField] float doorLiftTimeInterval;
    [SerializeField] Transform doorLiftPosition;

    MonkHealth[] monks;
    Vector3 startPos;
    float doorLiftStartTime;
    int monkCount;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnEnable()
    {
        monks = FindObjectsOfType<MonkHealth>();
        monkCount = monks.Length;
        foreach (var monk in monks)
            monk.OnKeyCreated += DecreaseMonkCount;
    }

    private void OnDisable()
    {
        foreach (var monk in monks)
            monk.OnKeyCreated -= DecreaseMonkCount;
    }

    void DecreaseMonkCount()
    {
        monkCount--;

        if (monkCount <= 0)
            StartCoroutine(LiftDoor());
    }

    IEnumerator LiftDoor()
    {
        doorLiftStartTime = Time.time;
        while(Time.time < doorLiftStartTime + doorLiftTimeInterval)
        {
            transform.position = Vector3.Lerp(transform.position, doorLiftPosition.position, Time.time / (doorLiftStartTime + doorLiftTimeInterval));
            yield return null;
        }
        transform.position = doorLiftPosition.position;
    }
}