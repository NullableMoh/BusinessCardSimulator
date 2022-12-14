using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRequirmentDoors : MonoBehaviour
{
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

    void LowerDoors()
    {

        Destroy(gameObject);
    }
}
