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

    private float _cameraBottomY;
    private float _minY;
    private float _minX;
    private float _maxY;
    private float _maxX;
    
    private readonly List<GameObject> _platforms = new();
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;

        if (_camera != null)
        {
            _cameraBottomY = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
            float highestPointY = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
            float lowestPointX = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            float highestPointX = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

            float platformWidth = platformPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
            float platformHeight = platformPrefab.GetComponent<SpriteRenderer>().bounds.extents.y;

            _minX = lowestPointX + platformWidth;
            _minY = _cameraBottomY + platformHeight;
            _maxX = highestPointX - platformWidth;
            _maxY = highestPointY - platformHeight;
        }
    }

    private void Update()
    {
        _cameraBottomY = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        _platforms.Where(x => x.transform.position.y < _cameraBottomY).ToList().ForEach(MovePlatform);
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

        Vector3 spawnPosition = new Vector3(0,_minY,0);
        
        for (int i = 0; i < initialPlatformCount; i++)
        {
            if (i == 0)
            {
                spawnPosition.x = player.transform.position.x;
            }
            else
            {
                spawnPosition.x = Random.Range(_minX, _maxX);
            }
            
            spawnPosition.y += Random.Range(bottomOffsetY, topOffsetY);
            GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            _platforms.Add(platform);
        }
    }

}