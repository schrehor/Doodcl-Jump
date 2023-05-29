using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public bool canSpawnPowerUp;
    public bool canSpawnMonster;
    // Add other shared properties here

    // Add shared methods here
    public void SpawnPowerUp()
    {
        // Your code for spawning a power up
    }

    public void SpawnMonster()
    {
        // Your code for spawning a monster
    }
}

