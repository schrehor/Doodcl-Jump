using UnityEngine;

namespace Platforms
{
    public class PlatformMovingY : Platform
    {
        public float moveSpeed = 1.5f;

        private float _minY;
        private float _maxY;
        private float _platformHeight;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _platformHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

            UpdateBounds();
        }

        private void UpdateBounds()
        {
            if (_camera != null)
            {
                float lowestPointY = _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
                float highestPointY = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

                _minY = lowestPointY + _platformHeight;
                _maxY = highestPointY - _platformHeight;
            }
        }

        private void Update()
        {
            transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
            UpdateBounds();

            if (transform.position.y > _maxY)
            {
                moveSpeed *= -1;
            }
            else if (transform.position.y < _minY)
            {
                moveSpeed *= -1;
            }
        }
    }
}
