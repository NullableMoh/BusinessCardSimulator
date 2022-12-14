using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverDisplay : MonoBehaviour
{
    [SerializeField] float timeTillDisplay;
    [SerializeField] GameObject over;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        over.SetActive(false);
        yield return new WaitForSeconds(timeTillDisplay);
        over.SetActive(true);

        FindObjectOfType<PlayerMovement>().enabled = false;
    }
}
