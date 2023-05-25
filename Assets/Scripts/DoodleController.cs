using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleController : MonoBehaviour
{
    [SerializeField] private float jumpStrength;

    private Rigidbody2D _rigidbody;
    
    void Start()
    {
          _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
             _rigidbody.velocity = Vector2.up * jumpStrength;
        }

        if (transform.position.y <= -2)
        {
            _rigidbody.velocity = Vector2.up * jumpStrength;
        }
       
    }
}
