using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 12f;
    [SerializeField] float AccelerationDueToGravity = -20f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float inAirSlowFactor = 0.1f;
    [SerializeField] AudioClip[] footsteps;
    
    float groundDistance = 0.4f;
    bool isGrounded;

    float xMovement, zMovement, soundStartTime;
    Vector3 velocity, moveVec, velBeforeNotGrounded;
    PlayerInputActions inputActions;

    AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
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

        moveVec = (transform.right * xMovement + transform.forward * zMovement).normalized;
        
        if(inputActions.Player.Jump.IsPressed() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * AccelerationDueToGravity);
        }

        velocity.y += AccelerationDueToGravity * Time.deltaTime;

        if (isGrounded)
        {
            velBeforeNotGrounded = moveVec;

            if(moveVec.magnitude != 0)
            {
                PlaySound(footsteps[Random.Range(0, footsteps.Length)]);
            }
        }


        if (!isGrounded)
        {
            moveVec = velBeforeNotGrounded;
            if (Mathf.Abs((moveVec + transform.right * xMovement * inAirSlowFactor).x) <= 1)
            {
                moveVec += transform.right * xMovement * inAirSlowFactor;
            }

            if (Mathf.Abs((moveVec + transform.forward * zMovement * inAirSlowFactor).z) <= 1)
            {
                moveVec += transform.forward * zMovement * inAirSlowFactor;
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        if(Time.time > soundStartTime)
        {
            audioSource.clip = clip;
            soundStartTime = Time.time + clip.length;
            audioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        controller.Move(moveVec * moveSpeed * Time.fixedDeltaTime);
        controller.Move(velocity * Time.fixedDeltaTime);
    }
}
