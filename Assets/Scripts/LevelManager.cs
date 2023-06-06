using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Start()
    {
        player.OnPlayerDeath += HandlePlayerDeath;
        GetComponent<PlatformSpawner>().Initialize(player);
    }

    private void HandlePlayerDeath()
    {
        SceneManager.LoadScene("Menu");
    }
}

