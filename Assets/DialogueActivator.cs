using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueActivator : MonoBehaviour
{

    PlayerInputActions inputActions;

    public event Action OnTryActivateDialogue;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }

    private void OnEnable()
    {
        inputActions.Player.DialogueInteract.performed += TryDialogueInteract;
    }

    void TryDialogueInteract(InputAction.CallbackContext callbackContext)
    {
        var raycastHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out var hit, LayerMask.GetMask(Strings.Layers.Citizen) | LayerMask.GetMask(Strings.Layers.Dialogue));
        if(raycastHit && hit.transform.gameObject.CompareTag("Citizen"))
        {
            OnTryActivateDialogue?.Invoke();
        }
    }
}
