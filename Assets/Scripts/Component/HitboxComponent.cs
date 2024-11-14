using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitboxComponent : MonoBehaviour
{
    private HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        if (healthComponent == null)
        {
            Debug.LogError("HealthComponent not found on " + gameObject.name);
        }
    }

    public void Damage(Bullet bullet)
    {
        if (bullet != null)
        {
            healthComponent.Subtract(bullet.damage);
        }
    }

    public void Damage(int damage)
    {
        healthComponent.Subtract(damage);
    }
}
