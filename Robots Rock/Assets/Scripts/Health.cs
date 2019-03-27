﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    //Set in Inspector
    public HealthConfig healthConfig;
    public UnityEvent onDeath;
    //

    public int CurrentHealth { get; private set; }

    public void Die()
    {
        if (onDeath != null)
        {
            onDeath.Invoke();
        }

        Debug.Log(transform.name + " died!");
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Awake()
    {
        CurrentHealth = healthConfig.maxHealth;
    }
}