using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class MoneyPresenter : MonoBehaviour
{
    [SerializeField] bool moneyRequiredToExit;

    int totalMoney;
    
    NextLevel[] nextLevel;

    TextMeshProUGUI text;
    MoneyCollector moneyCollector;

    public event Action OnMoneyRequirmentMet;

    void OnEnable()
    {
        moneyCollector = FindObjectOfType<MoneyCollector>();
        moneyCollector.OnMoneyCollected += UpdateMoneyCount;

        nextLevel = FindObjectsOfType<NextLevel>();
        foreach (var level in nextLevel)
            level.OnLoadNextLevel += SaveMoney;
    }

    private void OnDisable()
    {
        moneyCollector.OnMoneyCollected -= UpdateMoneyCount;

        foreach (var level in nextLevel)
            level.OnLoadNextLevel -= SaveMoney;
    }

    private void Awake()
    {
        totalMoney = LoadMoney();
    }

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        
        if (text)
        {
            if (moneyRequiredToExit)
            {
                text.text = $"${totalMoney} / $500";
            }
            else
            {
                text.text = $"${totalMoney}";
            }
        }
    }

    void UpdateMoneyCount(int moneyValue)
    {
        Debug.Log("Money count updated");
        totalMoney += moneyValue;

        if (!text) return;

        if (moneyRequiredToExit)
        {
            text.text = $"${totalMoney} / $500";
            if(totalMoney >= 500)
            {
                OnMoneyRequirmentMet?.Invoke();
            }
        }
        else
        {
            text.text = $"${totalMoney}";
        }
    }

    void SaveMoney()
    {
        BinaryFormatter formatter = new();
        var path = Application.persistentDataPath + "/money.data";
        FileStream stream = new(path, FileMode.Create);
        
        formatter.Serialize(stream, totalMoney);
        stream.Close();
    }

    int LoadMoney()
    {
        var path = Application.persistentDataPath + "/money.data";
        if (!File.Exists(path)) return 0;

        BinaryFormatter formatter = new();
        FileStream stream = new(path, FileMode.Open);

        var money = (int) formatter.Deserialize(stream);
        stream.Close();

        return money;
    }

}
