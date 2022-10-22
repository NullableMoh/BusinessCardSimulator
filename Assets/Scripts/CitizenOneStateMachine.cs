using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenOneStateMachine : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float walkSpeed, runSpeed, gunSensitivityRange;
    [SerializeField] int randomTurnMinAngle, randomTurnMaxAngle,
                        randomTurnMinTime, randomTurnMaxTime,
                        wallHitRandomTurnMinAngle, wallHitRandomTurnMaxAngle;
    [SerializeField] AudioClip walkingSound;


    string currentAnimState;
    float currentSpeed;
    UsableItem[] usableItems;
    Transform playerTransform;

    AudioSource audioSource;
    Rigidbody rb;


    private void OnEnable()
    {
        usableItems = FindObjectsOfType<UsableItem>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;

        foreach(var usableItem in usableItems)
            usableItem.OnItemUsed += TryRun;
    }

    private void OnDisable()
    {
        foreach (var usableItem in usableItems)
            usableItem.OnItemUsed -= TryRun;
    }

    private void Awake()
    {
        currentSpeed = walkSpeed;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlaySound(walkingSound));
    }

    private void FixedUpdate()
    {
        rb.velocity = currentSpeed * transform.forward * Time.fixedDeltaTime;
    }

    void TryRun(float recoil)
    {
        if(Vector3.Distance(playerTransform.position, transform.position) < gunSensitivityRange)
        {
            currentSpeed = runSpeed;
            PlayAnimation(Strings.Animations.CitizenOne.Running);
        }
    }

    void PlayAnimation(string newState)
    {
        if(currentAnimState != newState)
        {
            currentAnimState = newState;
            animator.Play(currentAnimState);
        }
    }

    IEnumerator PlaySound(AudioClip clip)
    {
        while (true)
        {
            audioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Strings.Layers.Ground))
        {
            transform.Rotate(0f, Random.Range(wallHitRandomTurnMinAngle, wallHitRandomTurnMaxAngle), 0f);
        }
    }
}
