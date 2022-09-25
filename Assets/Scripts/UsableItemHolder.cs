using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItemHolder : MonoBehaviour
{
    UsableItem currentItem;

    PlayerInputActions inputActions;
    void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }
    
    void Update()
    {
        if (!currentItem) return;

        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<UsableItem>())
        {
            Debug.Log("Usable Item");
            currentItem = other.GetComponent<UsableItem>();
            currentItem.transform.parent = transform;
            currentItem.transform.localPosition = Vector3.zero;
            currentItem.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
}
