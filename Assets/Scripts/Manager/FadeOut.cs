using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour 
{
    public static FadeOut Instance;

    public SpriteRenderer fadeSprite;
    public TextMeshProUGUI gameOverText;
    private GameManager _gameManager;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            FadeAndLoadGameOver();
            
        }
    }

    private void Start()
    {
        Instance = this;
        _gameManager = GameManager.Instance;

    }
    
    public void FadeAndLoadGameOver()
    {
        StartCoroutine(FadeToGameOver());
    }

    public IEnumerator FadeToGameOver()
    {
        yield return new WaitForSeconds(0.3f);
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
}