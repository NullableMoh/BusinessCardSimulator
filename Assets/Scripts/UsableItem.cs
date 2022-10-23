using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class UsableItem : MonoBehaviour, IUsableItem
{
    public abstract void UseItem(InputAction.CallbackContext callbackContext);
    public abstract event Action<float> OnItemUsed;
}
