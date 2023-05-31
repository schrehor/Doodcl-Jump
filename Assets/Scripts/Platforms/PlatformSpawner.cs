using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int initialPlatformCount;

    private float minYGap;
    private float maxYGap;
    private float minPlatformY;
    private float maxPlatformY;
    private float halfPlatformWidth;

    public void Initialize(PlayerController player)
    {
        // Calculate the min and max Y gap based on the player's maximum jump height
        float maxJumpHeight = player.GetJumpHeight();
        

        SpawnInitialPlatforms(maxJumpHeight);
    }

    private void SpawnInitialPlatforms(float offset)
    {
        float lowestPointy = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        float highestPointy = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float lowestPointx = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float highestPointx = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        float platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
        float platformHeight = platformPrefab.GetComponent<SpriteRenderer>().bounds.extents.y;
        
        Instantiate(platformPrefab, new Vector3(lowestPointx + platformWidth, lowestPointy + platformHeight, 0), Quaternion.identity);
        Instantiate(platformPrefab, new Vector3(lowestPointx + platformWidth, highestPointy - platformHeight, 0), Quaternion.identity);
        Instantiate(platformPrefab, new Vector3(highestPointx - platformWidth, lowestPointy + platformHeight, 0), Quaternion.identity);
        Instantiate(platformPrefab, new Vector3(highestPointx - platformWidth, highestPointy - platformHeight, 0), Quaternion.identity);
        
        Instantiate(platformPrefab, new Vector3(0, lowestPointy + platformHeight, 0), Quaternion.identity);
        Instantiate(platformPrefab, new Vector3(1, lowestPointy + platformHeight + offset, 0), Quaternion.identity);
        Instantiate(platformPrefab, new Vector3(2, lowestPointy + platformHeight + offset * 2, 0), Quaternion.identity);
    }

}