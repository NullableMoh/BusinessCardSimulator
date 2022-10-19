using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class UsableItem : MonoBehaviour, IUsableItem
{
    public abstract void UseItem();
    public abstract event Action<float> OnUse;
}
