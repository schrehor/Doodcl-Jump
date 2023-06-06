using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] platformPrefabs;
        [SerializeField] private int initialPlatformCount;

        public float bottomOffsetY = .5f;
        public float topOffsetY = 2f;

        private float _cameraBottomY;
        private float _minY;
        private float _minX;
        private float _maxY;
        private float _maxX;
    
        private readonly List<GameObject> _pathPlatforms = new();
        private readonly List<GameObject> _extraPlatforms = new();
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

                float platformWidth = platformPrefabs[0].GetComponent<SpriteRenderer>().bounds.extents.x;
                float platformHeight = platformPrefabs[0].GetComponent<SpriteRenderer>().bounds.extents.y;

                _minX = lowestPointX + platformWidth;
                _minY = _cameraBottomY + platformHeight;
                _maxX = highestPointX - platformWidth;
                _maxY = highestPointY - platformHeight;
            }
        }

        private void Update()
        {
            _cameraBottomY = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
            _pathPlatforms.Where(x => x.transform.position.y < _cameraBottomY).ToList().ForEach(MovePlatform);
        }

        private void MovePlatform(GameObject platform)
        {
            float lastHeight = _pathPlatforms.Max(x => x.transform.position.y);

            if (platform.GetComponent<Platform>() is PlatformBreakable plat)
            {
                plat.
                int a = 5;
            }
            
            if (Random.Range(0,10) == 0)
            {
                DestroyPlatform(platform);
                GenerateSpecialPlatform(lastHeight);
            }
            else
            {
                platform.GetComponent<Platform>().Reset();
                platform.transform.position = new Vector3(Random.Range(_minX, _maxX), lastHeight + Random.Range(bottomOffsetY, topOffsetY), 0);
            }
        }

        private void DestroyPlatform(GameObject platform)
        {
            _pathPlatforms.Remove(platform);
            Destroy(platform);
        }

        private void GenerateSpecialPlatform(float lastHeight)
        {
            //Random.Range(0, platformPrefabs.Length)
            GameObject platform1 = Instantiate(platformPrefabs[1], new Vector3(Random.Range(_minX, _maxX), lastHeight + Random.Range(bottomOffsetY, topOffsetY), 0), Quaternion.identity);
            GameObject platform2 = Instantiate(platformPrefabs[0], new Vector3(Random.Range(_minX, _maxX), lastHeight + Random.Range(bottomOffsetY, topOffsetY), 0), Quaternion.identity);
            _pathPlatforms.Add(platform1);
            _pathPlatforms.Add(platform2);
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
                GameObject platform = Instantiate(platformPrefabs[0], spawnPosition, Quaternion.identity);
                _pathPlatforms.Add(platform);
            }
        }

    }
}