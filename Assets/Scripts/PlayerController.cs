using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform targetRotation;
    [SerializeField] Joystick m_joystick;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
   // AudioSource m_AudioSource;
    public float turnSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
     //   m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = m_joystick.Horizontal;
        float vertical = m_joystick.Vertical;
        float x = GetX();
       
        //   print(targetRotation.localEulerAngles.x);
         if (targetRotation.transform.localEulerAngles.x > 270 )
         {

           horizontal *= -1;
        vertical *= -1;
       }
        
      //  float horizontal = Input.GetAxis("Horizontal");
      //  float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool iswalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", iswalking);
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        if (iswalking)
        {
           // if (!m_AudioSource.isPlaying)
         //   {
         //       m_AudioSource.Play();
          //  }
        }
        else
        {
           // m_AudioSource.Stop();
        }
    }

    private float GetX()
    {
        Vector3 angles = targetRotation.transform.eulerAngles;
        float x = targetRotation.transform.eulerAngles.x;
        float y = targetRotation.transform.eulerAngles.y;
        float z = targetRotation.transform.eulerAngles.z;
        if (Vector3.Dot(transform.up, Vector3.up) >= 0f)
        {
            if (angles.x >= 0f && angles.x <= 90f)
            {
                x = angles.x;
            }
            if (angles.x >= 270f && angles.x <= 360f)
            {
                x = angles.x - 360f;
            }
        }
        if (Vector3.Dot(transform.up, Vector3.up) < 0f)
        {
            if (angles.x >= 0f && angles.x <= 90f)
            {
                x = 180 - angles.x;
            }
            if (angles.x >= 270f && angles.x <= 360f)
            {
                x = 180 - angles.x;
            }
        }


        return x;

    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
