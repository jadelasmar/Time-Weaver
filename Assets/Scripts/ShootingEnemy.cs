using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShootinEnemy : MonoBehaviour
{
    public bool attacking;
    
    private RaycastHit2D _hit;
    private Animator _animator;
    private Vector2 _destination;
    private GameObject _projectile;
    private Transform _player;


    [SerializeField] private int shootOffset;
    [SerializeField] private float rayLength = 5f;
    [SerializeField] private float shootTime = 2f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _projectile = transform.GetChild(0).gameObject;
        _destination = new Vector2(_projectile.transform.position.x + shootOffset, _projectile.transform.position.y);
    }

    private void Update()
    {
        if (!attacking)
        {
            _hit = Physics2D.Raycast(transform.position, transform.right, rayLength, 1<<LayerMask.NameToLayer("Player"));
            _player = _hit.transform;
            if (_hit.collider != null && _hit.collider.gameObject.CompareTag("Player"))
            {
                attacking = true;
                ShootAnim();
            }
        }
    }

    

    private void ShootAnim()
    {
        _animator.SetTrigger("Attack");
    }

    private void Shoot()
    {
        _projectile.SetActive(true);
        _projectile.GetComponent<Ball>().Shooting(_destination,shootTime,_player);
    }
}