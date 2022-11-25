using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsableItemSlotSelector : MonoBehaviour
{
    [SerializeField] UsableItemSlot[] itemSlots;

    int currentItemSlotIndex;
    bool canSwitchSlot;

    Shotgun shotgun;
    StoneLeg stoneLeg;

    private void OnEnable()
    {
        shotgun = FindObjectOfType<Shotgun>();
        if (shotgun)
        {
            shotgun.OnShotgunCharging += DisableSwitchSlot;
            shotgun.OnItemUsed += EnableSwitchSlot;
        }

        stoneLeg = FindObjectOfType<StoneLeg>();
        if(stoneLeg)
        {
            stoneLeg.OnStoneLegKickStarted += DisableSwitchSlot;
            stoneLeg.OnItemUsed += EnableSwitchSlot;
        }
    }

    private void OnDisable()
    {
        if (shotgun)
        {
            shotgun.OnShotgunCharging -= DisableSwitchSlot;
            shotgun.OnItemUsed -= EnableSwitchSlot;
        }

        if (stoneLeg)
        {
            stoneLeg.OnStoneLegKickStarted -= DisableSwitchSlot;
            stoneLeg.OnItemUsed -= EnableSwitchSlot;
        }
    }

    void Awake()
    {
        currentItemSlotIndex = 0;
        canSwitchSlot = true;
    }


    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i == currentItemSlotIndex)
            {
                itemSlots[i].gameObject.SetActive(true);
            }
            else
            {
                itemSlots[i].gameObject.SetActive(false);
            }
        }
    }

    
    void Update()
    {
        if (!canSwitchSlot) return;

        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown((KeyCode)(i + 49)))
            {
                currentItemSlotIndex = i;
                for (int q = 0; q < itemSlots.Length; q++)
                {
                    if (q == currentItemSlotIndex)
                    {
                        itemSlots[q].gameObject.SetActive(true);
                    }
                    else
                    {
                        itemSlots[q].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void DisableSwitchSlot()
    {
        canSwitchSlot = false;
    }
    void EnableSwitchSlot(float _)
    {
        canSwitchSlot = true;
    }
}
