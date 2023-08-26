using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour 
{
    public SpriteRenderer fadeSprite;
    public TextMeshProUGUI gameOverText;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;

    }

    public IEnumerator FadeToGameOver()
    {
        float alpha = 0f;
        
        while (alpha < 1f) 
        {
            alpha += Time.deltaTime*2f;
            fadeSprite.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        
        StartCoroutine(ScaleText());
    }
    
    IEnumerator ScaleText() {

        gameOverText.enabled = true;
  
        float scale = 0f;
        while (scale < 1f)
        {
            scale += Time.deltaTime*2f;
            gameOverText.transform.localScale = Vector3.one * scale;
            yield return null;
        }
        
        yield return new WaitForSeconds(1f);
        
        _gameManager.GameOver();
    }

    public void FadeAndLoadGameOver()
    {
        StartCoroutine(FadeToGameOver());
    }
}