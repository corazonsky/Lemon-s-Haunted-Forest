using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;



public class LevelFinish : MonoBehaviour
{

    public static event Action OnEndLevel;
    public static event Action OnCollectMoreLemon;
    
    CanvasGroup m_SuccessBackground;
    AudioSource m_SuccessAudio;
    PickupSpawner m_Spawner;
    Transform m_Player;

    bool m_HasAudioPlayed;
    float m_LemonCount = 0;
    int m_TotalLemon;
    bool m_HasAllLemonCollected = false;

    private void Awake()
    {
        Pickup.OnPickupGrab += UnlockLevel;
    }

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_SuccessBackground = GameObject.FindGameObjectWithTag("Success Background").GetComponent<CanvasGroup>();
        m_SuccessAudio = GameObject.FindGameObjectWithTag("Success Audio").GetComponent<AudioSource>();
        m_Spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PickupSpawner>();
        m_TotalLemon = m_Spawner.numberOfPickups;
    }


    private void OnDestroy()
    {
        Pickup.OnPickupGrab -= UnlockLevel;
    }

    void UnlockLevel()
    {
        m_LemonCount++;
        if(m_LemonCount == m_TotalLemon)
        {
            m_HasAllLemonCollected = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.transform == m_Player)
        {
            Debug.Log("Masuk");
            if (m_HasAllLemonCollected)
            {
                EndLevel(m_SuccessBackground, m_SuccessAudio);
                OnEndLevel?.Invoke();
            }
            else
            {
                Debug.Log("Collect lemons!");
                OnCollectMoreLemon?.Invoke();
            }
        }
    }



    void EndLevel(CanvasGroup imageCanvasGroup, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        imageCanvasGroup.alpha = 255;
    }
}
