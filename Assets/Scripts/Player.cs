using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
   private void Start()
    {
        LeanTween.scale(gameObject, Vector2.one, 0.5f);
    }

   public void EnableMovement(bool move)
   {
       GetComponent<PlayerMovement2D>().canMove = move;
   }
}
