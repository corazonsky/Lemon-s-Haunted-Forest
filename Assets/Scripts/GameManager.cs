using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GetManager()
    {
        return currentManager;
    }

    static GameManager currentManager = null;


    public int lives;
    public float respawnTime;

    public int currentLevel = 1;

    private void Awake()
    {
        if(currentManager == null)
        {
            currentManager = this;
            DontDestroyOnLoad(gameObject);
            Observer.OnPlayerCaught += PlayerCaught; //call event if subscribed
            LevelFinish.OnEndLevel += PlayerFinish;
            GameOver.OnGameOver += Reset;
            PauseMenu.OnQuit += Reset;
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    private void OnDestroy()
    {
        if(currentManager == this)
        {
            currentManager = null;
            Observer.OnPlayerCaught -= PlayerCaught; //unsubscribe
            LevelFinish.OnEndLevel -= PlayerFinish;
            GameOver.OnGameOver -= Reset;
            PauseMenu.OnQuit -= Reset;
        }
    }
    
    void PlayerCaught()
    {
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        Cursor.lockState = CursorLockMode.Confined;
        lives--;
        if (lives > 0)
        {
            Debug.Log(lives);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //restart screen
        }
        else
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("Game Over");
        }
    }

    void PlayerFinish()
    {
        Invoke("NextLevel", respawnTime);
    }

    void NextLevel()
    {
        Cursor.lockState = CursorLockMode.Confined;
        currentLevel++;
        if(currentLevel <= 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Game Over");
        }
        
    }

    private void Reset()
    {
        Destroy(gameObject);
    }
}
