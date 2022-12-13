using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAfterTime : MonoBehaviour
{
    [SerializeField] float timeTillInstantiate;
    [SerializeField] GameObject objectToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiateObject());
    }

    IEnumerator InstantiateObject()
    {
        yield return new WaitForSeconds(timeTillInstantiate);
        Instantiate(objectToInstantiate, transform.position, Quaternion.identity);
    }
}
