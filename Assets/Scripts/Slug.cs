using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slug : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float xOffset = 7f;

    private Vector2 _startPos;
    private Vector2 _targetPos;

    private int _moveDirection = 1;

    private SpriteRenderer _sprite;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {

        _targetPos = _moveDirection == 1 ? _startPos : new Vector2(_startPos.x - xOffset, _startPos.y);

        transform.position = Vector2.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

        if (transform.position.x == _targetPos.x)
        {
            _moveDirection *= -1;
            _sprite.flipX = (_moveDirection == 1);
        }
    }
}