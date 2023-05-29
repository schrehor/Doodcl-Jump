using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platform;

    public int NumberOfPlatforms { get; set; } = 10;

    // 1 najprv spawnut prvych 10 platforiem do okna bez toho aby sa touchovali
    // 2 kontrolovat ci nejaka nespadla pod obrazovku a ak hej tak ju presunut nad nech postupne spadne
    
    public GameObject[] platformPrefabs;
    public int numberOfPlatforms = 10;
    public float minY = -6f, maxY = 6f;

    void Start()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnRandomPlatform();
        }
    }

    void Update()
    {
        // Check if there are less than `numberOfPlatforms` in the scene
        GameObject[] existingPlatforms = GameObject.FindGameObjectsWithTag("Platform");
        if (existingPlatforms.Length < numberOfPlatforms)
        {
            SpawnRandomPlatform();
        }
    }

    void SpawnRandomPlatform()
    {
        // Choose a random platform type
        GameObject platformPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        // Choose a random position
        float x = Random.Range(-2.5f, 2.5f);
        float y = Random.Range(minY, maxY);
        Vector2 position = new Vector2(x, y);

        // Instantiate the platform
        Instantiate(platformPrefab, position, Quaternion.identity);
    }
}
