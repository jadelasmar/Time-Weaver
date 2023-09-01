using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision2D : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
}