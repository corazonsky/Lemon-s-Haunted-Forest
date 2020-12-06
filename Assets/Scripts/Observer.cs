using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour
{
    public static event Action OnPlayerCaught;

    CanvasGroup m_CaughtBackground;
    AudioSource m_CaughtAudio;
    Transform m_Player;

    bool m_IsPlayerInRange;
    bool m_IsCaught = false;

    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        m_CaughtBackground = GameObject.FindGameObjectWithTag("Caught Background").GetComponent<CanvasGroup>();
        m_CaughtAudio = GameObject.FindGameObjectWithTag("Caught Audio").GetComponent<AudioSource>();

    }

    //check if player go inside or outside trigger
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == m_Player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.transform == m_Player)
        {
            m_IsPlayerInRange = false;
        }
    }

    //check if the sight line is clear
    void FixedUpdate()
    {
        if (m_IsPlayerInRange && !m_IsCaught)
        {
            //shoot ray on direction between player and observer
            Vector3 direction = m_Player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            //check if the ray hit something
            if(Physics.Raycast(ray, out raycastHit))
            {
                //check if it hits the player (not the wall)
                if(raycastHit.collider.transform == m_Player)
                {
                    m_IsCaught = true;
                    OnPlayerCaught?.Invoke(); //is there any listener
                    EndLevel(m_CaughtBackground, m_CaughtAudio);
                }
            }
        }    
    }

    bool m_HasAudioPlayed = false;


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
