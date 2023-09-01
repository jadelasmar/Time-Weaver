using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public PlayerController2D controller;
    public bool canMove;
    
    private Animator _animator;

    public float runSpeed = 40f;
    
    private float _horizontalMove = 0f;
    private bool _jump = false;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(canMove)
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
            if (Input.GetButtonDown("Jump"))
            {
                _jump = true;
                _animator.SetBool("isJumping", true);
            }
        }
    }

    public void OnLanding()
    {
        _animator.SetBool("isJumping",false);
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            controller.Move(_horizontalMove * Time.fixedDeltaTime, _jump);
            _jump = false;
        }
    }
    

   
}
