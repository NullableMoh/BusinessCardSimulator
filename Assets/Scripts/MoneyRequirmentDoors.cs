using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRequirmentDoors : MonoBehaviour
{
    [SerializeField] float doorDistance, doorLiftTimeInterval;

    float doorLiftStartTime;
    Vector3 doorLiftPostion;
    Coroutine doorLowering;

    MoneyPresenter moneyPresenter;

    private void OnEnable()
    {
        moneyPresenter = FindObjectOfType<MoneyPresenter>();
        moneyPresenter.OnMoneyRequirmentMet += LowerDoors;
    }

    private void OnDisable()
    {
        moneyPresenter.OnMoneyRequirmentMet -= LowerDoors;
    }

    private void Awake()
    {
        doorLiftPostion = transform.position + Vector3.up * doorDistance;
    }

    void LowerDoors()
    {
        doorLowering = StartCoroutine(Lower());
        Debug.Log("doors should lower");
    }

    IEnumerator Lower()
    {
        Debug.Log("doors lowering");
        doorLiftStartTime = Time.time;
        while (Time.time < doorLiftStartTime + doorLiftTimeInterval)
        {
            transform.position = Vector3.Lerp(transform.position, doorLiftPostion, Time.time / (doorLiftStartTime + doorLiftTimeInterval));
            yield return null;
        }
        transform.position = doorLiftPostion;
    }
}
