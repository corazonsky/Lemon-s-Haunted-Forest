using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{ 
    public static event Action OnPickupGrab;
    public float rotateSpeed;
    public AudioClip pickupSound;
    public AudioClip grabbedSound;
    public float pickupVolume;
    public float grabbedVolume;

    AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.Rotate(0f, rotateSpeed, 0f);
        if (!m_AudioSource.isPlaying)
        {
            PlaySound(pickupSound, pickupVolume);
        }
    }

    public void PlaySound(AudioClip audioClip, float Volume)
    {
        m_AudioSource.volume = Volume;
        m_AudioSource.PlayOneShot(audioClip);
    }

    void Grab()
    {
        Debug.Log("Pickup Grabbed");
        PlaySound(grabbedSound, grabbedVolume);
        Destroy(gameObject, grabbedSound.length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Grab();
            OnPickupGrab?.Invoke();
        }
    }
}