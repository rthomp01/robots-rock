using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    //Set in Inspector
    public HealthConfig healthConfig;
    public UnityEvent onDeath;
    public UnityEvent onHit;
    //

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = healthConfig.maxHealth;

        if (transform != transform.root)
        {
            Debug.LogWarning("WARNING: If health component is not on the root transform, damage will not work correctly.");
        }
    }

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

        if(onHit != null)
        {
            onHit.Invoke();
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
}
