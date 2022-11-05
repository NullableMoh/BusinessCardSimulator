using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenOneStateMachine : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float walkSpeed, runSpeed, gunSensitivityRange, turnMinTime, turnMaxTime;
    [SerializeField] int turnMinAngle, turnMaxAngle;
    [SerializeField] AudioClip walkingSound;


    string currentAnimState;
    float currentSpeed;
    UsableItem[] usableItems;
    Transform playerTransform;

    CitizenOneMoneySpawner moneySpawner;
    AudioSource audioSource;
    Rigidbody rb;
    CapsuleCollider col;


    private void OnEnable()
    {
        usableItems = FindObjectsOfType<UsableItem>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
        moneySpawner = GetComponent<CitizenOneMoneySpawner>();
        moneySpawner.CitizenOneHitByPlayerProjectile += Run;

        foreach(var usableItem in usableItems)
            usableItem.OnItemUsed += TryRun;
    }

    private void OnDisable()
    {
        moneySpawner.CitizenOneHitByPlayerProjectile -= Run;

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
        col = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlaySound(walkingSound));
        StartCoroutine(TurnRandomly());
    }

    IEnumerator TurnRandomly()
    {
        while (true)
        {
            transform.Rotate(0f, Random.Range(turnMinAngle, turnMinAngle), 0f);
            yield return new WaitForSeconds(Random.Range(turnMinTime, turnMaxTime));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = ((Vector3.up * -100f) + currentSpeed * transform.forward) * Time.fixedDeltaTime;
    }

    void TryRun(float recoil)
    {
        if (Vector3.Distance(playerTransform.position, transform.position) >= gunSensitivityRange) return;
        
        Run();
    }

    void Run()
    {
        currentSpeed = runSpeed;
        PlayAnimation(Strings.Animations.CitizenOne.Running);
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
}
