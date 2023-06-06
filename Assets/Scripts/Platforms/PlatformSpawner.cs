using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int initialPlatformCount;

    private float _minY;
    private float _minX;
    private float _maxY;
    private float _maxX;

    private void Awake()
    {
        float lowestPointY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        float highestPointY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float lowestPointX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float highestPointX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        float platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
        float platformHeight = platformPrefab.GetComponent<SpriteRenderer>().bounds.extents.y;

        _minX = lowestPointX + platformWidth;
        _minY = lowestPointY + platformHeight;
        _maxX = highestPointX - platformWidth;
        _maxY = highestPointY - platformHeight;
    }

    public void Initialize(PlayerController player)
    {
        float maxJumpHeight = player.GetJumpHeight();

        SpawnInitialPlatforms(player);
    }

    private void SpawnInitialPlatforms(PlayerController player)
    {
        // Instantiate(platformPrefab, new Vector3(_minX, _minY, 0), Quaternion.identity);
        // Instantiate(platformPrefab, new Vector3(_minX, _maxY, 0), Quaternion.identity);
        // Instantiate(platformPrefab, new Vector3(_maxX, _minY, 0), Quaternion.identity);
        // Instantiate(platformPrefab, new Vector3(_maxX, _maxY, 0), Quaternion.identity);
        //
        // Instantiate(platformPrefab, new Vector3(0, _minY, 0), Quaternion.identity);
        // Instantiate(platformPrefab, new Vector3(1, _minY + offset, 0), Quaternion.identity);
        // Instantiate(platformPrefab, new Vector3(2, _minY + offset * 2, 0), Quaternion.identity);

        Vector3 spawnposition = new Vector3(0,_minY,0);
        
        for (int i = 0; i < initialPlatformCount; i++)
        {
            if (i == 0)
            {
                spawnposition.x = player.transform.position.x;
            }
            else
            {
                spawnposition.x = Random.Range(_minX, _maxX);
            }
            
            spawnposition.y += Random.Range(.5f, 2f);
            Instantiate(platformPrefab, spawnposition, Quaternion.identity);
        }
    }

}