using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    public event Action<int> OnMoneyCollected;

    private void OnTriggerEnter(Collider other)
    {
        var money = other.gameObject.GetComponent<Money>();
        if(money)
        {
            OnMoneyCollected?.Invoke(money.Value);
            Destroy(other.gameObject);
        }
    }
}
