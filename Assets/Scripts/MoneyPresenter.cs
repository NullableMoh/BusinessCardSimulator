using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyPresenter : MonoBehaviour
{
    int moneyCount;

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
        moneyCount = 0;
    }

    void UpdateMoneyCount()
    {
        moneyCount++;
        label.text = "$ " + moneyCount.ToString();
    }
}
