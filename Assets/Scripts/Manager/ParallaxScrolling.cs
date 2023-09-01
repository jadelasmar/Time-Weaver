using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] private GameObject viewTarget;
    [SerializeField] private float parallax;
    [SerializeField] private bool lockY;
    private float length, xStartPos,yStartPos;
    private void Start()
    {
        xStartPos = transform.position.x;
        yStartPos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = viewTarget.transform.position.x * (1 - parallax);
        float dist = viewTarget.transform.position.x * parallax;
        
        if (lockY)
        {
            transform.position = new Vector3(xStartPos + dist, viewTarget.transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(xStartPos + dist, transform.position.y, transform.position.z);

        }
        
        if (temp > xStartPos + length)
            xStartPos += length;
        else if (temp < xStartPos - length)
            xStartPos -= length;
        
    }
}