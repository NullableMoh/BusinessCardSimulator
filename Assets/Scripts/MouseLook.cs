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

    PlayerInputActions inputActions;

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

        playerBody.Rotate(0f, mouseX, 0f);

        //if (transform.rotation.x >= -0.5f && transform.rotation.x <= 0.5f)
            transform.Rotate(-mouseY, 0f, 0f);
        //else
            //transform.Rotate(-Mathf.Sign(transform.rotation.x)*1f, 0f, 0f);
    }
}
