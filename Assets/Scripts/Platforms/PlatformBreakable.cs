using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Platforms
{
    public class PlatformBreakable : Platform
    {
        private Animator _animator;
        private Sprite _sprite;
        
        private static readonly int Break = Animator.StringToHash("Break");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>().sprite;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.GetComponent<Rigidbody2D>().velocity.y < 0)
                    _animator.SetTrigger(Break);
            }
        }
        
        private void DisablePlatform()
        {
            gameObject.SetActive(false);
        }

        private new void Reset()
        {
            GetComponent<SpriteRenderer>().sprite = _sprite;
            gameObject.SetActive(true);
        }
    }
}
