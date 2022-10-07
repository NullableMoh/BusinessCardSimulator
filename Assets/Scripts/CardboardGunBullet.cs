using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardGunBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;

    public Vector3 HitPoint;
    public Vector3 Direction;
    Rigidbody rb;
    // Start is called before the first frame update

    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody>();

        yield return new WaitForEndOfFrame();
        transform.parent = null;
        Direction = (HitPoint - transform.position).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = Direction * bulletSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
