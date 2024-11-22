using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Level; // Level musuh
    public EnemySpawner enemySpawner; // Referensi ke spawner musuh
    public CombatManager combatManager; // Referensi ke CombatManager

    private void OnDestroy()
    {
        // Ketika musuh dihancurkan, panggil fungsi dari EnemySpawner dan CombatManager
        if (enemySpawner != null && combatManager != null)
        {
            enemySpawner.OnDeath(); // Beritahu spawner bahwa musuh telah dikalahkan
            combatManager.RegisterKill(); // Tambahkan jumlah musuh yang dikalahkan
        }
    }

    private void Start()
    {
        // Inisialisasi dasar untuk Enemy
        gameObject.SetActive(false); // Nonaktifkan Enemy di awal
    }

    private void Update()
    {
        // Logika tambahan untuk Enemy bisa diletakkan di sini
    }

    // Fungsi untuk mengaktifkan Enemy setelah waktu tertentu
    public IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Tunggu selama waktu yang ditentukan
        gameObject.SetActive(true); // Aktifkan Enemy
    }
}
