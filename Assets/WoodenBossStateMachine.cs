using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBossStateMachine : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float walkSpeed, runSpeed, gunSensitivityRange, turnMinTime, turnMaxTime;
    [SerializeField] int turnMinAngle, turnMaxAngle, health;
    [SerializeField] AudioClip walkingSound, runningSound;
    [SerializeField] GameObject damageParticleSystem;

    string currentAnimState;
    float currentSpeed;
    int initialHealth;
    UsableItem[] usableItems;
    Transform playerTransform;

    AudioSource audioSource;
    Rigidbody rb;
    CapsuleCollider col;


    private void OnEnable()
    {
        usableItems = FindObjectsOfType<UsableItem>();
        playerTransform = FindObjectOfType<PlayerMovement>().transform;

        foreach (var usableItem in usableItems)
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
        initialHealth = health;
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
        Debug.Log("Try Run");
        if (Vector3.Distance(playerTransform.position, transform.position) >= gunSensitivityRange) return;
        Run();
    }

    void Run()
    {
        currentSpeed = runSpeed;
        PlayAnimation(Strings.Animations.CitizenOne.Running);
        StopCoroutine(PlaySound(walkingSound));
        StartCoroutine(PlaySound(runningSound));
    }

    void PlayAnimation(string newState)
    {
        if (currentAnimState != newState)
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerProjectile>())
        {
            Run();

            if(health > 0)
            {
                health--;
                Instantiate(damageParticleSystem, transform.position, Quaternion.identity);
            }
            else
            {
                Destroy(gameObject);
            }    
        }
    }
}
