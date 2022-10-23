using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardGunBullet : PlayerProjectile
{
    [SerializeField] float bulletSpeed, destroyTimeAfterHit;

    bool hitPointDirection;
    Vector3 hitPoint, direction, initialPosition;

    Collider col;
    Rigidbody rb;
    // Start is called before the first frame update

    private void Awake()
    {
        hitPointDirection = false;
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
        if(hitPointDirection)
            direction = (hitPoint - initialPosition).normalized;
    
        rb.velocity = direction * bulletSpeed * Time.fixedDeltaTime;
    }

    public override void CalculateDirection(RaycastHit hit, bool raycastHit, CardboardGun gun)
    {
        if(raycastHit)
        {
            hitPointDirection = true;
            hitPoint = hit.point;
        }
        else
        {
            hitPointDirection = false;
            direction = Camera.main.transform.forward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        col.enabled = false;
        transform.parent = other.transform;

        Destroy(gameObject, destroyTimeAfterHit);
    }
}
