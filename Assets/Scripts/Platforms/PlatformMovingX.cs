using UnityEngine;

namespace Platforms
{
    public class PlatformMovingX : Platform
    {
        public float moveSpeed = 1.5f; 

        private float _minX; 
        private float _maxX; 
        private float _platformWidth;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;

            if (_camera != null)
            {
                float lowestPointX = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
                float highestPointX = _camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

                _platformWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

                _minX = lowestPointX + _platformWidth;
                _maxX = highestPointX - _platformWidth;
            }
        }

        private void Update()
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            
            if (transform.position.x > _maxX)
            {
                moveSpeed *= -1;
            }
            else if (transform.position.x < _minX)
            {
                moveSpeed *= -1;
            }
        }
    }
}
