using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsableItemSlot : MonoBehaviour
{
    UsableItem childUsableItem;
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }

    private void OnEnable()
    {
        inputActions.Player.PickUpUsableitem.performed += TrySwitchItem;
    }

    private void OnDisable()
    {
        inputActions.Player.PickUpUsableitem.performed -= TrySwitchItem;
    }

    private void Start()
    {
        childUsableItem = GetComponentInChildren<UsableItem>();
    }

    private void TrySwitchItem(InputAction.CallbackContext callbackContext)
    {
        var raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, Mathf.Infinity, LayerMask.GetMask(Strings.Layers.UsableItem));
        if (raycastHit)
        {
            var usableItem = hit.transform.gameObject.GetComponent<UsableItem>();
            if (usableItem)
            {
                usableItem.transform.parent = transform;
                usableItem.transform.localPosition = Vector3.zero;
                usableItem.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

                childUsableItem.transform.parent = null;
                childUsableItem = GetComponentInChildren<UsableItem>();
            }
        }
    }
}
