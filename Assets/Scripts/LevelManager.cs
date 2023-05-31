using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void Start()
    {
        GetComponent<PlatformSpawner>().Initialize(player);
    }

}

