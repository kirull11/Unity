using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float MaxHealthPoints;

    public float currentHealth;

    public Action<float> HealthChanged;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (HealthChanged != null)
            {
                currentHealth = value;
                HealthChanged.Invoke(currentHealth);
            }
        }
    }

    void Start()
    {
        CurrentHealth = MaxHealthPoints;
        
    }

    
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Debug.Log("Player is dead");
            Time.timeScale = 0;
            return;
        }
    }
}
