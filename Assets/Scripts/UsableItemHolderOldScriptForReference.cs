/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsableItemHolderOldScriptForReference : MonoBehaviour
{
    [SerializeField] UsableItemSlot[] itemSlots;

    int currentItemSlotIndex;

    void Awake()
    {
        currentItemSlotIndex = 0;
    }
    
    void Update()
    {
        for(int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown((KeyCode)(i + 49)))
            {
                currentItemSlotIndex = i;
                for(int q = 0; q < itemSlots.Length; q++)
                {
                    if(i == q)
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

        currentItem = itemSlots[currentItemSlotIndex].CurrentUsableItem;

        if (!currentItem) return;
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
    }

    void UseCurrentItem(InputAction.CallbackContext callbackContext)
    {
        if (!currentItem) return;

        currentItem.UseItem();
        OnItemUsed?.Invoke();
    }

    void PickUpUsableItem(InputAction.CallbackContext callbackContext)
    {
        if (!canPickUpUsableItem) return;

        if(currentItem)
        {
            currentItem.transform.parent = null;
            currentItem.transform.position = new Vector3(currentItem.transform.position.x, transform.position.y + 1, currentItem.transform.position.z);
            currentItem = null;
        }

        currentItem = pickUpableItem;
        currentItem.transform.parent = itemSlots[currentItemSlotIndex].transform;
        OnItemPickedUp?.Invoke(currentItem.GetComponent<UsableItem>());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UsableItem>())
        {
            canPickUpUsableItem = true;
            pickUpableItem = other.GetComponent<UsableItem>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<UsableItem>())
        {
            canPickUpUsableItem = false;
            pickUpableItem = null;
        }
    }
}*/