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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fall"))
        {
            gameObject.SetActive(false);
            FadeOut.Instance.FadeAndLoadGameOver();
        }
        
        if (other.gameObject.name == ("Start Portal"))
        {
            Invoke(nameof(enableMovement), (float)0.5);
        }
    }

    private void enableMovement()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<PlayerMovement2D>().enabled = true;
        GetComponent<CharacterController2D>().enabled = true;
    }

    IEnumerator MoveToTarget(Transform portal)
    {
        Vector3 portalCenter = portal.GetComponent<Collider2D>().bounds.center;

        while (Vector2.Distance(transform.position, portalCenter) > 0.1f) {

            Vector2 direction = (portalCenter - transform.position).normalized;

            _rigidbody2D.AddForce(direction * 2);
  
            yield return null;
        }

        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<PlayerMovement2D>().enabled = false;
        GetComponent<CharacterController2D>().enabled = false;
        _gameManager.LevelComplete();
    }
}