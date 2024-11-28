using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;  // Bullet digunakan untuk menyerang
    public int damage;     // Damage yang diberikan oleh objek

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hitbox = GetComponent<HitboxComponent>();  // Mengambil komponen HitboxComponent
        if (collision.gameObject.tag == gameObject.tag)
        {
            return; // Jika objek bertabrakan dengan objek yang sama, keluar
        }
        if (collision.CompareTag("Bullet"))
        {
            // Mendapatkan damage dari Bullet yang mengenai objek ini

            if (hitbox != null)
            {
                hitbox.Damage(collision.GetComponent<Bullet>()); // Menerapkan damage dengan parameter Bullet dari objek yang terkena
                Debug.Log("Bullet damage applied.");
            }
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
        {
            hitbox = GetComponent<HitboxComponent>();  // Mengambil komponen HitboxComponent lagi untuk tipe objek lain
            if (hitbox != null)
            {
                hitbox.Damage(damage);  // Menerapkan damage langsung menggunakan nilai damage yang ditentukan
                Debug.Log("Direct damage applied.");
                
                var invincibility = collision.GetComponent<InvicibiltyComponent>();  // Cek apakah objek memiliki komponen Invincibility
                if (invincibility != null)
                {
                    invincibility.StartInvincibility(); // Memulai efek invincibility pada objek yang terkena
                    Debug.Log("Invincibility started for collided object.");
                }
            }
        }
    }
}
