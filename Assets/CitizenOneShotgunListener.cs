using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenOneShotgunListener : MonoBehaviour
{
    [SerializeField] GameObject dismantledCitizenOne;
    Shotgun shotgun;

    private void OnEnable()
    {
        shotgun = FindObjectOfType<Shotgun>();
        shotgun.OnShotgunFired += CitizenOneTryRegisterHit;
    }
    private void OnDisable()
    {
        shotgun.OnShotgunFired -= CitizenOneTryRegisterHit;
    }

    void CitizenOneTryRegisterHit(GameObject hitObject)
    {
        if(hitObject == gameObject)
        {
            Instantiate(dismantledCitizenOne, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
