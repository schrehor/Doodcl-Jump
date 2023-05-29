using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpStrength;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _speed;
    private static readonly int Direction = Animator.StringToHash("Direction");
    private static readonly int Jumping = Animator.StringToHash("Jumping");

    void Start()
    {
          _rigidbody = GetComponent<Rigidbody2D>();
          _animator = GetComponent<Animator>();
          _speed = jumpStrength / 2;
    }
    
    void Update()
    {
        // todo toto je len na testovanie potom treba vymazat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = Vector2.up * jumpStrength;
        }

        Move();
        ResetJumpTrigger();
    }

    private void ResetJumpTrigger()
    {
        if (_rigidbody.velocity.y <= 0)
        {
            _animator.ResetTrigger(Jumping);
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");

        // Obrati hraca podla smeru pohybu
        Vector3 scale = transform.localScale;
        if (moveX > 0)
        {
            scale.x = -1;
            _animator.SetFloat(Direction, 1f);
        }
        else if (moveX < 0)
        {
            scale.x = 1;
            _animator.SetFloat(Direction, -1f);
        }
        else
        {
            _animator.SetFloat(Direction, 0f);
        }
        transform.localScale = scale;
        
        // Pohyb hraca
        Vector2 newPosition = transform.position;
        newPosition.x += moveX * _speed * Time.deltaTime;
        transform.position = newPosition;
    }

    public float GetJumpHeight()
    {
        return Mathf.Pow(jumpStrength, 2) / (2 * _rigidbody.gravityScale);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _animator.SetTrigger(Jumping);
            _rigidbody.velocity = Vector2.up * jumpStrength;
        }
    }
}
