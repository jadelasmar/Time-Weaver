using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonMovingEntity : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.GetComponent<Player>().EnableMovement(false);
            FadeOut.Instance.FadeAndLoadGameOver();
        }
    }
}
