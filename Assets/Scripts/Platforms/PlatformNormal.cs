using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformNormal : Platform
{
    //private BoxCollider2D _boxCollider;

    public Vector3 Size { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        canSpawnMonster = true;
        //_boxCollider = GetComponent<BoxCollider2D>();
        //Size = _boxCollider.size * transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
