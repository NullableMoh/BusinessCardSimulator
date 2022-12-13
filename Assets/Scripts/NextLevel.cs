using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public event Action OnLoadNextLevel;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<NextLevelTrigger>();
        if (player)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        OnLoadNextLevel?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
