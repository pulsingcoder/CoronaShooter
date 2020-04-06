using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSConroller : MonoBehaviour
{
    AudioSource fps_Audio;
    private const float maxHealth = 20f;
    private float currentHealth = 0f;

    public CharacterController controller;
    public float speed = 12f;
    [SerializeField] Joystick m_joystick;

    // Update is called once per frame
    void Start()
    {
        currentHealth = maxHealth;
        fps_Audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        float horizontal = Mathf.Abs(m_joystick.Horizontal);
        float vertical = Mathf.Abs(m_joystick.Vertical);

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
        if (horizontal != 0 || vertical != 0)
        {
            if (!fps_Audio.isPlaying)
            {
                fps_Audio.Play();
            }
        }
        else
        {
            fps_Audio.Stop();
        }
   
        
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (hit.collider.gameObject.tag == "door")
        {
            SceneManager.LoadScene(2);
        }
       
    }

    public void HurtMe(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <=0)
        {
            SceneManager.LoadScene(2);
        }
    }


}
