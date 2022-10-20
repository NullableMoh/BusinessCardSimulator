using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardGunBullet : PlayerProjectile
{
    [SerializeField] float bulletSpeed, destroyTimeAfterHit;

    public bool ManualDirectionOverride;
    public Vector3 HitPoint;
    public Vector3 Direction;
    Vector3 initialPosition;

    Collider col;
    Rigidbody rb;
    // Start is called before the first frame update

    private void Awake()
    {
        ManualDirectionOverride = false;
    }

    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        yield return new WaitForEndOfFrame();
        transform.parent = null;
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        if(!ManualDirectionOverride)
            Direction = (HitPoint - initialPosition).normalized;
    
        rb.velocity = Direction * bulletSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        col.enabled = false;
        transform.parent = other.transform;

        Destroy(gameObject, destroyTimeAfterHit);
    }
}
