using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOver : MonoBehaviour { 
public static event Action OnGameOver;

    private void Start()
    {
        OnGameOver?.Invoke();
    }
    
    public void QuitGame()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

}
