using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyPresenter : MonoBehaviour
{
    int totalMoney;

    Label label;
    MoneyCollector moneyCollector;

    // Start is called before the first frame update
    void OnEnable()
    {
        var uIDoc = GetComponent<UIDocument>();
        var rootVisualElement = uIDoc.rootVisualElement;
        label = rootVisualElement.Q<Label>();

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

    void UpdateMoneyCount(int moneyValue)
    {
        totalMoney += moneyValue;
        label.text = "$ " + totalMoney.ToString();
    }
}
