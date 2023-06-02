using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > transform.position.y)
        {
            var position = transform.position;
            position = new Vector3(position.x, player.position.y, position.z);
            transform.position = position;
        }
    }
}
