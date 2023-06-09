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
    private Camera _camera;
    private float _screenWidth;
    
    private static readonly int Direction = Animator.StringToHash("Direction");
    private static readonly int Jumping = Animator.StringToHash("Jumping");

    public Action OnPlayerDeath;

    void Awake()
    {
          _rigidbody = GetComponent<Rigidbody2D>();
          _animator = GetComponent<Animator>();
          _speed = jumpStrength / 2;
          _camera = Camera.main;
          if (_camera != null) _screenWidth = _camera.orthographicSize * 2f * _camera.aspect;
    }
    
    void Update()
    {
        CheckDeath();
        WrapAroundScreen();
        
        // todo toto je len na testovanie potom treba vymazat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = Vector2.up * jumpStrength;
        }

        //todo na move pouzit fixed update
        Move();
        ResetJumpTrigger();
    }

    private void CheckDeath()
    {
        if (transform.position.y < _camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y)
        {
            OnPlayerDeath?.Invoke();
        }
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
        
        // Pohyb hraca old
        // Vector2 newPosition = transform.position;
        // newPosition.x += moveX * _speed * Time.deltaTime;
        // transform.position = newPosition;
        
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = moveX * _speed;
        _rigidbody.velocity = velocity;
    }

    public float GetJumpHeight()
    {
        float jumpHeight = Mathf.Pow(jumpStrength, 2) / (2 * _rigidbody.gravityScale);
        float viewportHeight = Camera.main.orthographicSize * 2f;
        float jumpHeightViewport = jumpHeight / viewportHeight;
        
        return jumpHeightViewport;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (collision.relativeVelocity.y > 0)
            { 
                _animator.SetTrigger(Jumping);
                _rigidbody.velocity = Vector2.up * jumpStrength;
            }
        }
    }
    
    void WrapAroundScreen()
    {
        Vector3 currentPosition = transform.position;

        if (currentPosition.x < -_screenWidth / 2f)
        {
            currentPosition.x = _screenWidth / 2f;
        }
        else if (currentPosition.x > _screenWidth / 2f)
        {
            currentPosition.x = -_screenWidth / 2f;
        }

        transform.position = currentPosition;
    }

}
