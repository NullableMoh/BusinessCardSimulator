using System;
using UnityEngine.InputSystem;

interface IUsableItem
{
    void UseItem(InputAction.CallbackContext callbackContext);
    public event Action<float> OnItemUsed;
}
