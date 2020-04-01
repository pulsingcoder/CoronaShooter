using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour
{
    public Transform m_player;
    bool isPlayerInRange;
    public float damage  = 5f;
    public float impactForce = 100f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == m_player)
        {
            isPlayerInRange = true;
           
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == m_player)
        {
            isPlayerInRange = false;
        }
    }

        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange)
        {
            Vector3 direction = m_player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == m_player)
                {

                    m_player.GetComponent<FPSConroller>().HurtMe(damage);
                    ApplyForce(raycastHit);
                }
            }
        }
        
    }

    private void ApplyForce(RaycastHit raycastHit)
    {
        Rigidbody m_rigidBody = GetComponent<Rigidbody>();
        if (m_rigidBody != null)
        {
            m_rigidBody.AddForce(raycastHit.normal * impactForce);
        }
    }
}
