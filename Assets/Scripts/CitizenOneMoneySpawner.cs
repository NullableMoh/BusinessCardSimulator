using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenOneMoneySpawner : MonoBehaviour
{
    [SerializeField] GameObject moneyCluster;
    [SerializeField] int totalMoneySpawns;

    int remainingMoneySpawns;

    private void Awake()
    {
        remainingMoneySpawns = totalMoneySpawns;
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("trigger");
        if (collider.gameObject.GetComponent<PlayerProjectile>() && remainingMoneySpawns > 0)
        {
            remainingMoneySpawns--;
            Instantiate(moneyCluster, transform.position, Quaternion.identity);
        }
    }
}
