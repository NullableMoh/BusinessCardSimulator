using System;
using UnityEngine.InputSystem;

interface IUsableItem
{
    void UseItem();
    public event Action<float> OnUse;
}
