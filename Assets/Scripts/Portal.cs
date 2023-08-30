using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Portal : MonoBehaviour
{
    [SerializeField] private bool end;
    [SerializeField] private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(end ? Teleport(other) : Close());
        }
    }

    private IEnumerator Teleport(Collider2D playerCol)
    {
        //Get center of portal
        var targetPos = GetComponent<CircleCollider2D>().bounds.center;

        // Disable portal collision and player movement
        GetComponent<Collider2D>().enabled = false;
        player.GetComponent<Player>().EnableMovement(false);
        playerCol.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


        // Scale player to 0 while rotating around itself and moving toward portal
        LeanTween.scale(playerCol.gameObject, Vector2.zero, 1f);
        LeanTween.rotateAround(playerCol.gameObject, UnityEngine.Vector3.forward, 360f, 1f);
        LeanTween.move(playerCol.gameObject, targetPos, 1f);

        yield return new WaitForSeconds(0.5f);

        // Scale portal to 0 while rotating around itself
        LeanTween.scale(gameObject, Vector2.one * 1.5f, 0.5f).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, Vector2.zero, 1f);
            LeanTween.rotateAround(gameObject, UnityEngine.Vector3.forward, 360f, 1f);
        });

        // Wait for Tween
        while (LeanTween.isTweening(gameObject))
        {
            yield return null;
        }
        
        // Notify Level Complete
        GameManager.Instance.LevelComplete();
    }

    private IEnumerator Close()
    {
        // Disable Portal
        GetComponent<Collider2D>().enabled = false;

        //Enable player movement
        player.GetComponent<Player>().EnableMovement(true);

        // Scale portal to 1.5
        LeanTween.scale(gameObject, Vector2.one * 1.5f, 0.5f).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, Vector2.zero, 1f);
            LeanTween.rotateAround(gameObject, UnityEngine.Vector3.forward, 360f, 1f);
        });

        yield return null;
    }
}