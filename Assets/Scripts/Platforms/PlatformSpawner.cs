using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int initialPlatformCount;

    public float bottomOffsetY = .5f;
    public float topOffsetY = 2f;

    private float _cameraBottom;
    private float _minY;
    private float _minX;
    private float _maxY;
    private float _maxX;
    
    private List<GameObject> _platforms = new List<GameObject>();

    private void Awake()
    {
        _cameraBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        float highestPointY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float lowestPointX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float highestPointX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        float platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
        float platformHeight = platformPrefab.GetComponent<SpriteRenderer>().bounds.extents.y;

        _minX = lowestPointX + platformWidth;
        _minY = _cameraBottom + platformHeight;
        _maxX = highestPointX - platformWidth;
        _maxY = highestPointY - platformHeight;
    }

    private void Update()
    {
        _cameraBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        _platforms.Where(x => x.transform.position.y < _cameraBottom).ToList().ForEach(MovePlatform);
    }

    private void MovePlatform(GameObject platform)
    {
        float lastHeight = _platforms.Max(x => x.transform.position.y);
        
        platform.transform.position = new Vector3(Random.Range(_minX, _maxX), lastHeight + Random.Range(bottomOffsetY, topOffsetY), 0);
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
            
            spawnposition.y += Random.Range(bottomOffsetY, topOffsetY);
            GameObject platform = Instantiate(platformPrefab, spawnposition, Quaternion.identity);
            _platforms.Add(platform);
        }
    }

}