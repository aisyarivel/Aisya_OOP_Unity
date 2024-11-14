using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();

        if (hitbox != null)
        {
            hitbox.Damage(bullet);

            // Jika ingin menggunakan integer damage, gunakan baris di bawah ini dan pastikan metode Damage mendukung integer.
            // hitbox.Damage(damage);
        }
    }
}
