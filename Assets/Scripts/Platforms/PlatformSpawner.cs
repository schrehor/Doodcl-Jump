using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class PlatformSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] platformPrefabs;
        private const int PathPlatformCount = 15;
        private const int ExtraPlatformCount = 5;

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
            platform.transform.position = new Vector3(Random.Range(_minX, _maxX), lastHeight + Random.Range(bottomOffsetY, topOffsetY), 0);
            
            if (Random.Range(0,10) == 0)
            {
                // DestroyPlatform(platform);
                // GenerateSpecialPlatform(lastHeight);
            }
            else
            {
                
            }
        }

        private void MoveExtraPlatforms()
        {
            GetComponent<Platform>().Reset();
        }
        
        private void SpawnExtraPlatforms()
        {
            var y1 = _pathPlatforms.Min(x => x.transform.position.y);
            var y2 = _pathPlatforms.Max(x => x.transform.position.y);
            
            for (int i = 0; i < ExtraPlatformCount; i++)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(_minX, _maxX), Random.Range(y1, y2), 0);
                if (CheckCollisions(spawnPoint))
                {
                    GameObject platform = Instantiate(platformPrefabs[1], spawnPoint, Quaternion.identity);
                    _extraPlatforms.Add(platform);
                }
            }
        }
        
        private bool CheckCollisions(Vector3 spawnPoint)
        {
            List<GameObject> platforms = _pathPlatforms.Concat(_extraPlatforms).ToList();

            float platformHeight = platformPrefabs[0].GetComponent<SpriteRenderer>().bounds.extents.y;
            float platformWidth = platformPrefabs[0].GetComponent<SpriteRenderer>().bounds.extents.x;

            Vector3 raycastOffset = new Vector3(0, platformHeight, 0);

            foreach (var platform in platforms)
            {
                Vector3 existingPosition = platform.transform.position;
                Vector2 existingSize = platform.GetComponent<SpriteRenderer>().bounds.size;
                
                float existingHalfWidth = existingSize.x / 2f;
                float existingHalfHeight = existingSize.y / 2f;
                
                Vector3 leftRaycastOrigin = spawnPoint + raycastOffset + new Vector3(-existingHalfWidth, 0, 0);
                Vector3 rightRaycastOrigin = spawnPoint + raycastOffset + new Vector3(existingHalfWidth, 0, 0);
                
                Vector2 leftRaycastDirection = Vector2.down;
                Vector2 rightRaycastDirection = Vector2.down;
                
                RaycastHit2D leftHit = Physics2D.Raycast(leftRaycastOrigin, leftRaycastDirection, platformHeight);
                RaycastHit2D rightHit = Physics2D.Raycast(rightRaycastOrigin, rightRaycastDirection, platformHeight);
                
                if (leftHit.collider != null || rightHit.collider != null)
                {
                    return false;
                }
            }

            return true;
        }


        private void DestroyPlatform(GameObject platform)
        {
            _pathPlatforms.Remove(platform);
            Destroy(platform);
        }

        private void GenerateSpecialPlatform(float lastHeight)
        {
            GameObject platform = Instantiate(platformPrefabs[1], new Vector3(Random.Range(_minX, _maxX), 
                lastHeight + Random.Range(bottomOffsetY, topOffsetY), 0), Quaternion.identity);
            _pathPlatforms.Add(platform);
        }

        public void Initialize(PlayerController player)
        {
            float maxJumpHeight = player.GetJumpHeight();

            SpawnInitialPlatforms(player);
        }

        private void SpawnInitialPlatforms(PlayerController player)
        {
            Vector3 spawnPosition = new Vector3(0,_minY,0);
        
            for (int i = 0; i < PathPlatformCount; i++)
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
            
            //SpawnExtraPlatforms();
        }
    }
}