using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : MonoBehaviour
{
    private bool _attacking = false;
    private RaycastHit2D _hit;
    private Animator _animator;
    private Vector2 _initialPos;
    private Vector2 _destination;
    private GameObject _projectile;

    [SerializeField] private int shootOffset;
    [SerializeField] private float rayLength = 5f;
    [SerializeField] private float shootTime = 2f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        _projectile = transform.GetChild(0).GetChild(0).gameObject;
        _initialPos = _projectile.transform.position;
        _destination = new Vector2(_initialPos.x + shootOffset, _initialPos.y);
    }

    private void Update()
    {
        if (!_attacking)
        {
            _hit = Physics2D.Raycast(transform.position, transform.right, rayLength, 1<<LayerMask.NameToLayer("Player"));
            if (_hit.collider != null && _hit.collider.gameObject.CompareTag("Player"))
            {
                _attacking = true;
                ShootAnim();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _projectile.SetActive(false);
            FadeOut.Instance.FadeAndLoadGameOver();
        }
    }

    private void ShootAnim()
    {
        _animator.SetTrigger("Attack");
    }

    private IEnumerator Shoot()
    {
        _projectile.SetActive(true);
        LeanTween.move(_projectile, _destination, shootTime);
        while (LeanTween.isTweening(_projectile))
        {
            yield return null;
        }
        _projectile.SetActive(false);
        _projectile.transform.position = _initialPos;
        _attacking = false;


    }
}