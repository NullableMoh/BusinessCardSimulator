using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardboardGun : UsableItem
{
    public override void UseItem()
    {
        Debug.Log("Shot");
    }
}
