using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLegAnimatorEvents : MonoBehaviour
{
    public event Action OnKickAnimFinished;

    public void InvokeKickAnimFinished()
    {
        OnKickAnimFinished?.Invoke();
    }
}
