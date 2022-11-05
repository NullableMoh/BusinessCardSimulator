using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenOneMoneySpawner : MonoBehaviour
{
    [SerializeField] GameObject moneyCluster;
    [SerializeField] int totalMoneySpawns;

    int remainingMoneySpawns;

    public event Action CitizenOneHitByPlayerProjectile;

    private void Awake()
    {
        remainingMoneySpawns = totalMoneySpawns;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<PlayerProjectile>() && remainingMoneySpawns > 0)
        {
            remainingMoneySpawns--;
            CitizenOneHitByPlayerProjectile?.Invoke();
            Instantiate(moneyCluster, transform.position, Quaternion.identity);
        }
    }
}
