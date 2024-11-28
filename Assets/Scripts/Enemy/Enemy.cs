using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Level;
    public EnemySpawner enemySpawner;
    public CombatManager combatManager;

    private void OnDestroy()
    {
        // Jika enemySpawner dan combatManager ada, lakukan tindakan berikut saat musuh dihancurkan
        if (enemySpawner != null && combatManager != null)
        {
            enemySpawner.OnDeath(); // Panggil metode OnDeath() di enemySpawner
            combatManager.RegisterKill(Level); // Catat kill dengan level musuh saat ini
        }
    }

    void Start()
    {
        // Inisialisasi musuh agar tidak aktif di awal permainan
        gameObject.SetActive(false); // Nonaktifkan musuh pada saat start
    }

    void Update()
    {
        // Dapat menambahkan logika musuh tambahan di sini jika diperlukan
    }

    // Fungsi untuk mengaktifkan musuh setelah menunggu waktu tertentu
    public IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Tunggu sesuai waktu yang ditentukan
        gameObject.SetActive(true); // Aktifkan objek musuh setelah delay
    }
}
