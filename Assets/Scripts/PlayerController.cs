using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpStrength;

    private Rigidbody2D _rigidbody;
    
    void Start()
    {
          _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // todo toto je len na testovanie potom treba vymazat
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = Vector2.up * jumpStrength;
        }
    }

    public float GetJumpHeight()
    {
        return Mathf.Pow(jumpStrength, 2) / (2 * _rigidbody.gravityScale);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _rigidbody.velocity = Vector2.up * jumpStrength;
            //_rigidbody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
        }
    }
}
