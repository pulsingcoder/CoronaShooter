using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Observer : MonoBehaviour
{
    public Transform m_player;
    bool isPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform == m_player)
        {
            isPlayerInRange = true;
            SceneManager.LoadScene(1);
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
        Vector3 direction = m_player.position - transform.position + Vector3.up;
        Ray ray = new Ray(transform.position, direction);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.transform == m_player)
            {
               
                SceneManager.LoadScene(1);
            }
        }

        
    }
}
