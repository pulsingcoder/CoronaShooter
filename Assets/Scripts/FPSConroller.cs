using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSConroller : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    [SerializeField] Joystick m_joystick;

    // Update is called once per frame
    void Update()
    {
        float horizontal = m_joystick.Horizontal;
        float vertical = Mathf.Abs(m_joystick.Vertical);

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * speed * Time.deltaTime);
    }
}
