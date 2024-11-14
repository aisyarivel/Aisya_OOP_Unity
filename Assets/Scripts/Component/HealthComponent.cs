using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public void Subtract(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float Health => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
}
