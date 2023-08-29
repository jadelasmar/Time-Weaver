using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class EndPortal : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D player)
    {
        StartCoroutine(Teleport(player));
    }

    IEnumerator Teleport(Collider2D player)
    {
        var targetPos = GetComponent<CircleCollider2D>().bounds.center;
        // Disable Portal
        GetComponent<Collider2D>().enabled = false;

        // Scale player to 0
        LeanTween.scale(player.gameObject, Vector2.zero, 1f);
        // Rotate player around self 
        LeanTween.rotateZ(player.gameObject, 180f, 1f);
        // Move player towards portal 
        LeanTween.move(player.gameObject, targetPos, 1f);

        // Wait for Tween
        while (LeanTween.isTweening(player.gameObject))
        {
            yield return null;
        }

        // Freeze Player
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        // Notify Level Complete
        GameManager.Instance.LevelComplete();
    }
}