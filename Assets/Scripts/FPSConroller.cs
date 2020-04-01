using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSConroller : MonoBehaviour
{
    private const float maxHealth = 20f;
    private float currentHealth = 0f;

    public CharacterController controller;
    public float speed = 12f;
    [SerializeField] Joystick m_joystick;

    // Update is called once per frame
    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        float horizontal = m_joystick.Horizontal;
        float vertical = Mathf.Abs(m_joystick.Vertical);

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
        Vector3 temp = controller.transform.localPosition;
        temp.y = 0;
        controller.transform.localPosition = temp;
        
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (hit.collider.gameObject.tag == "door")
        {
            SceneManager.LoadScene(1);
        }
       
    }

    public void HurtMe(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <=0)
        {
            SceneManager.LoadScene(1);
        }
    }


}
