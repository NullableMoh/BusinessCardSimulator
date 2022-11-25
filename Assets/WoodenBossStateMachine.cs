using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBossStateMachine : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float walkSpeed, runSpeed, gunSensitivityRange, turnMinTime, turnMaxTime;
    [SerializeField] int turnMinAngle, turnMaxAngle, health;
    [SerializeField] GameObject damageParticleSystem, runningSound, walkingSound, destroySound, stoneLeg;
    [SerializeField] AudioClip damageSound;

    string currentAnimState;
    float currentSpeed;
    int initialHealth;
    bool isRunning;
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
        isRunning = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(TurnRandomly());
        stoneLeg.SetActive(false);
    }

    IEnumerator TurnRandomly()
    {
        while (true)
        {
            transform.Rotate(0f, Random.Range(turnMinAngle, turnMinAngle), 0f);
            yield return new WaitForSeconds(Random.Range(turnMinTime, turnMaxTime));
        }
    }

    private void Update()
    {
        
        if (isRunning)
        {
            runningSound.SetActive(true);
            walkingSound.SetActive(false);
        }
        else
        {
            runningSound.SetActive(false);
            walkingSound.SetActive(true);
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
        isRunning = true;
    }

    void PlayAnimation(string newState)
    {
        if (currentAnimState == newState) return;

        currentAnimState = newState;
        animator.Play(currentAnimState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<PlayerProjectile>()) return;

        Run();

        if(health > 0)
        {
            health--;
            Instantiate(damageParticleSystem, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            audioSource.PlayOneShot(damageSound);
        }
        else
        {
            Instantiate(damageParticleSystem, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            Instantiate(destroySound);
            stoneLeg.SetActive(true);
            stoneLeg.transform.parent = null;
            Destroy(gameObject);
        }    
    }
}
