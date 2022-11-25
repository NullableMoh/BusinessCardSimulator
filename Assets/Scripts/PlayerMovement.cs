using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float AccelerationDueToGravity = -20f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] Transform groundCheck;
    
    float groundDistance = 0.4f;
    bool isGrounded;

    float xMovement, zMovement;
    Vector3 velocity, moveVec, moveVelBeforeJump;
    PlayerInputActions inputActions;

    CharacterController controller;

    private void Awake()
    {
        inputActions = new();
        inputActions.Player.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, LayerMask.GetMask("Ground"));
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //0f might leave player hovering in air.
        }

        xMovement = inputActions.Player.Movement.ReadValue<Vector2>().x;
        zMovement = inputActions.Player.Movement.ReadValue<Vector2>().y;

        moveVec = transform.right * xMovement + transform.forward * zMovement;
        
        if(inputActions.Player.Jump.IsPressed() && isGrounded)
        {
            moveVelBeforeJump = moveVec;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * AccelerationDueToGravity);
        }

        velocity.y += AccelerationDueToGravity * Time.deltaTime;

        if (!isGrounded)
        {
            moveVec = moveVelBeforeJump;
        }
    }

    private void FixedUpdate()
    {
        controller.Move(moveVec * moveSpeed * Time.fixedDeltaTime);
        controller.Move(velocity * Time.fixedDeltaTime);
    }
}
