using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision2D : MonoBehaviour
{
    private GameManager _gameManager;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _gameManager = GameManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            FadeOut.Instance.FadeAndLoadGameOver();
            StartCoroutine(EnableMovement(false));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fall"))
        {
            gameObject.SetActive(false);
            FadeOut.Instance.FadeAndLoadGameOver();
        }
        
        if (other.gameObject.name == ("Start Portal"))
        {
            StartCoroutine(EnableMovement(true));
        }
    }

    private IEnumerator EnableMovement(bool move)
    {
        if (move)
        {
            yield return new WaitForSeconds(0.5f);
            GetComponent<PlayerMovement2D>().enabled = true;
            GetComponent<CharacterController2D>().enabled = true;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            GetComponent<PlayerMovement2D>().enabled = false;
            GetComponent<CharacterController2D>().enabled = false;
        }
    }
}