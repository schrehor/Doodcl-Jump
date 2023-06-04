using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Late update aby sa kamera pohla az po tom ako sa pohne hrac
    void LateUpdate()
    {
        if (player.position.y > transform.position.y)
        {
            var position = transform.position;
            position = new Vector3(position.x, player.position.y, position.z);
            // SmoothDamp keby boli problemy
            transform.position = Vector3.Lerp(transform.position, position, 0.3f);
        }
    }
}
