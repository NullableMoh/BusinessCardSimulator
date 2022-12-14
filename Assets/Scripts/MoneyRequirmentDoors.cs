using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRequirmentDoors : MonoBehaviour
{
    [SerializeField] float doorDistance, doorLiftTimeInterval;

    float doorLiftStartTime;
    Vector3 doorLiftPostion;
    Coroutine doorLowering;
    bool doorsLowering;

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
        doorsLowering = false;
    }

    void LowerDoors()
    {
        if (doorsLowering) return;

        doorLowering = StartCoroutine(Lower());
        doorsLowering = true;
    }

    IEnumerator Lower()
    {
        doorLiftStartTime = Time.time;
        while (Time.time < doorLiftStartTime + doorLiftTimeInterval)
        {
            transform.position = Vector3.Lerp(transform.position, doorLiftPostion, Time.time / (doorLiftStartTime + doorLiftTimeInterval));
            yield return null;
        }
        transform.position = doorLiftPostion;
    }
}
