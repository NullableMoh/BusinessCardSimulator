using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsableItemHolder : MonoBehaviour
{
    [SerializeField] UsableItemSlot[] itemSlots;

    int currentItemSlotIndex;

    void Awake()
    {
        currentItemSlotIndex = 0;
    }

    private void Start()
    {
       for(int i = 0; i < itemSlots.Length; i++)
       {
            if(i == currentItemSlotIndex)
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
}
