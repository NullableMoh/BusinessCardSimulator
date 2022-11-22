using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyPresenter : MonoBehaviour
{
    int totalMoney;

    TextMeshProUGUI text;
    MoneyCollector moneyCollector;


    void OnEnable()
    {
        moneyCollector = FindObjectOfType<MoneyCollector>();
        moneyCollector.OnMoneyCollected += UpdateMoneyCount;
    }

    private void OnDisable()
    {
        moneyCollector.OnMoneyCollected -= UpdateMoneyCount;
    }

    private void Awake()
    {
        totalMoney = 0;
    }

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void UpdateMoneyCount(int moneyValue)
    {
        totalMoney += moneyValue;

        if (!text) return;
        text.text = $"${totalMoney}";
    }
}
