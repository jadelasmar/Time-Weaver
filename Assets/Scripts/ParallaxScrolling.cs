using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ParallaxScrolling : MonoBehaviour
{
    // [SerializeField] private float scrollSpeed = 0.3f;
    // [SerializeField] private float offset = 0f;
     [SerializeField] private GameObject viewTarget;
    // [SerializeField] private bool lockY;
    // private Tilemap _tilemap;
    // private float _xPos;
    // private float _yPos;

    private float length,startPos;
    public float parallax;
    
    

    private void Start()
    {
        //_tilemap = GetComponent<Tilemap>();
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = viewTarget.transform.position.x * (1 - parallax);
        float dist = viewTarget.transform.position.x * parallax;
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }

    private void Update()
    {
       // _xPos = viewTarget.transform.position.x * (scrollSpeed+offset);
       // _yPos = viewTarget.transform.position.y * (scrollSpeed+offset);
        
        //_tilemap.transform.position = lockY ? new Vector2(_xPos, _tilemap.transform.position.y) : new Vector2(_xPos, _yPos);
    }
}