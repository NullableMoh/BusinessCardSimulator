using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsableItemHolder : MonoBehaviour
{
    bool canPickUpUsableItem;

    UsableItem pickUpableItem;

    UsableItem currentItem;
    PlayerInputActions inputActions;
    
    void Awake()
    {
        canPickUpUsableItem = true;

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
    }

    void PickUpUsableItem(InputAction.CallbackContext callbackContext)
    {
        if (!canPickUpUsableItem) return;

        currentItem = null;
        currentItem.transform.parent = null;

        currentItem = pickUpableItem;
        currentItem.transform.parent = transform;
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
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
