using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UsableItemSlot : MonoBehaviour
{

    bool canSwitchItem;

    UsableItem childUsableItem;
    PlayerInputActions inputActions;

    Shotgun shotgun;
    StoneLeg stoneLeg;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
        canSwitchItem = true;
    }

    private void OnEnable()
    {
        inputActions.Player.PickUpUsableitem.performed += TrySwitchItem;

        shotgun = FindObjectOfType<Shotgun>();
        if (shotgun)
        {
            shotgun.OnShotgunCharging += DisableSwitchItem;
            shotgun.OnItemUsed += EnableSwitchItem;
        }

        stoneLeg = FindObjectOfType<StoneLeg>();
        if(stoneLeg)
        {
            stoneLeg.OnStoneLegKickStarted += DisableSwitchItem;
            stoneLeg.OnItemUsed += EnableSwitchItem;
        }
    }

    private void OnDisable()
    {
        inputActions.Player.PickUpUsableitem.performed -= TrySwitchItem;
        
        if (shotgun)
        {
            shotgun.OnShotgunCharging -= DisableSwitchItem;
            shotgun.OnItemUsed -= EnableSwitchItem;
        }

        if (stoneLeg)
        {
            stoneLeg.OnStoneLegKickStarted -= DisableSwitchItem;
            stoneLeg.OnItemUsed -= EnableSwitchItem;
        }
    }

    private void Start()
    {
        childUsableItem = GetComponentInChildren<UsableItem>();
    }

    void DisableSwitchItem()
    {
        canSwitchItem = false;
    }

    void EnableSwitchItem(float _)
    {
        canSwitchItem = true;
    }

    private void TrySwitchItem(InputAction.CallbackContext callbackContext)
    {
        if (!canSwitchItem) return;

        var raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, Mathf.Infinity, LayerMask.GetMask(Strings.Layers.UsableItem));
        if (raycastHit)
        {
            var usableItem = hit.transform.gameObject.GetComponent<UsableItem>();
            if (usableItem)
            {
                usableItem.transform.parent = transform;
                usableItem.transform.localPosition = Vector3.zero;
                usableItem.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

                if (childUsableItem)
                {
                    childUsableItem.transform.parent = null;
                }
                    childUsableItem = GetComponentInChildren<UsableItem>();
            }
        }
    }
}
