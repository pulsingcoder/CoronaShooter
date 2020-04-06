using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Text coronaLeft;
    public float Health = 5f;
    private int coronaCount = 7;
   
    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Die();
            coronaCount--;
            
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        
        coronaLeft.text = coronaCount.ToString();

    }

    
}
