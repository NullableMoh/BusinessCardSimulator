using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsableItemHolder : MonoBehaviour
{
    bool canPickUpUsableItem;

    UsableItem pickUpableItem;
    Quaternion lookRot;

    UsableItem currentItem;
    PlayerInputActions inputActions;

    public event Action OnItemUsed;
    public event Action<UsableItem> OnItemPickedUp;
    
    void Awake()
    {
        canPickUpUsableItem = false;

        inputActions = new();
        inputActions.Player.Enable();
        inputActions.Player.UseUsableItem.performed += UseCurrentItem;
        inputActions.Player.PickUpUsableitem.performed += PickUpUsableItem;
    }
    
    void Update()
    {
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
        currentItem.transform.parent = transform;
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
}
