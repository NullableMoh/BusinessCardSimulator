using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    public event Action OnMoneyCollected;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        if(other.gameObject.GetComponent<Money>())
            OnMoneyCollected?.Invoke();
    }
}
