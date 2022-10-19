using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{ 
    //config params
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;

    //state
    float xRot;
    UsableItem currentUsableItem;

    PlayerInputActions inputActions;

    UsableItemHolder holder;

    private void OnEnable()
    {
        holder = FindObjectOfType<UsableItemHolder>();
        holder.OnItemPickedUp += SwitchUsableItemForRecoil;
    }

    private void OnDisable()
    {
        holder.OnItemPickedUp -= SwitchUsableItemForRecoil;
        currentUsableItem.OnUse -= TakeRecoil;
    }

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = inputActions.Player.Look.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = inputActions.Player.Look.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        playerBody.Rotate(0f, mouseX, 0f);
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    void TakeRecoil(float recoilAmount)
    {
        xRot -= recoilAmount;
    }

    public void SwitchUsableItemForRecoil(UsableItem usableItem)
    {
        if (currentUsableItem)
            currentUsableItem.OnUse -= TakeRecoil;

        currentUsableItem = usableItem;
        currentUsableItem.OnUse += TakeRecoil;
    }
}
