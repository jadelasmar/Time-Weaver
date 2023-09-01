using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector2 _initialPosition;
    private LTDescr _tween;
    private Transform _player;
    private bool _facingRight;

    void Start()
    {
        _initialPosition = transform.position;
        if (transform.position.x > transform.parent.position.x)
            _facingRight = true;

    }

    public void Shooting(Vector2 destination, float shootTime, Transform player)
    {
        _player = player;
        _tween = LeanTween.move(gameObject, destination, shootTime).setOnUpdate(CheckCondition);
      

    }

    private void CheckCondition(float opt)
    {
        if (_facingRight)
        {
            if (transform.position.x > _player.position.x+3f)
            {
                ResetPos();
            }
        }
        else
        {
            if (transform.position.x < _player.position.x-3f)
            {
                ResetPos();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Explode");
            LeanTween.cancel(_tween.id);
        }
    }

    private void ResetPos()
    {
        LeanTween.cancel(_tween.id);
        transform.position = _initialPosition;
        gameObject.SetActive(false);
        transform.parent.GetComponent<ShootinEnemy>().attacking = false;
    }

    private void PlayerGameOver()
    {
        GameManager.Instance.GameOver();
    }

}
