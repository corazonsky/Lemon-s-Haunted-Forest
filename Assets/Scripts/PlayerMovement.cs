using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 10f;
    public float speed = 6f;
    public float mouseSensivity = 6f;

    public new Transform camera;
    public AudioClip footsteps;
    public float footstepsPitchRange;

    CinemachineFreeLook m_vCam;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Quaternion m_CurrentRotation;
    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        //initialize components
        m_vCam = FindObjectOfType<CinemachineFreeLook>();
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        //adjust mouse sensivity
        m_vCam.m_XAxis.m_MaxSpeed *= mouseSensivity;

    }

    public void PlaySound(AudioClip audioClip)
    {
        m_AudioSource.pitch = 1 + (Random.value * 2 - 1) * footstepsPitchRange;
        m_AudioSource.PlayOneShot(audioClip);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set the input movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        //set the animator bool if walking
        bool isWalking = m_Movement.magnitude >= 0.1f;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                PlaySound(footsteps);
            }
            //calculate degree between x and z
            float targetAngle = Mathf.Atan2(m_Movement.x, m_Movement.z) * Mathf.Rad2Deg + camera.eulerAngles.y;

            //set new rotation vector
            m_Rotation = Quaternion.Euler(0f, targetAngle, 0f);

            //set new move vector
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            moveDir.Normalize();
            m_Movement = moveDir;
        }
        else
        {
            m_AudioSource.Stop();
        }

        //rotate the player to wherever direction it is moving
        //Vector3 forward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.fixedDeltaTime, 0f);
        //m_Rotation = Quaternion.LookRotation(forward);
        m_CurrentRotation = Quaternion.Lerp(m_CurrentRotation, m_Rotation, turnSpeed * Time.fixedDeltaTime);
    }

    private void OnAnimatorMove()
    {
        //rotate and move the player after each animation state
        m_Rigidbody.MovePosition(m_Rigidbody.position + (m_Movement * speed) * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_CurrentRotation);
    }
}


