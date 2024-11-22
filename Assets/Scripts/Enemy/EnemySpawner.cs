using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefab Musuh")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3; // Jumlah minimum kill untuk meningkatkan jumlah spawn
    public int totalKill = 0; // Total kill yang dilakukan oleh pemain
    private int totalKillWave = 0; // Kill dalam gelombang saat ini

    [SerializeField] private float spawnInterval = 3f; // Waktu jeda antar spawn

    [Header("Counter Musuh yang Spawn")]
    public int spawnCount = 0; // Sisa jumlah musuh yang akan di-spawn pada gelombang saat ini
    public int defaultSpawnCount = 1; // Jumlah spawn dasar
    public int spawnCountMultiplier = 1; // Pengali untuk skala spawn
    public int multiplierIncreaseCount = 1; // Besar kenaikan multiplier

    public CombatManager combatManager; // Referensi ke CombatManager

    public bool isSpawning = false; // Apakah proses spawning sedang aktif

    private void Start()
    {
        spawnCount = defaultSpawnCount; // Inisialisasi jumlah spawn awal
    }

    public void StopSpawning()
    {
        isSpawning = false; // Menghentikan proses spawning
    }

    public void StartSpawning()
    {
        // Memulai spawning jika level musuh lebih kecil atau sama dengan nomor gelombang saat ini
        if (spawnedEnemy.Level <= combatManager.waveNumber)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    public IEnumerator SpawnEnemies()
    {
        if (isSpawning)
        {
            // Jika jumlah spawn 0, reset ke jumlah spawn dasar
            if (spawnCount == 0)
            {
                spawnCount = defaultSpawnCount;
            }

            int enemiesToSpawn = spawnCount;
            while (enemiesToSpawn > 0)
            {
                // Membuat instance musuh
                Enemy enemy = Instantiate(spawnedEnemy);
                enemy.GetComponent<Enemy>().enemySpawner = this;
                enemy.GetComponent<Enemy>().combatManager = combatManager; // Mengatur referensi CombatManager
                enemiesToSpawn--;
                spawnCount = enemiesToSpawn;

                // Menambahkan total jumlah musuh yang dikalahkan di CombatManager
                if (combatManager != null)
                {
                    combatManager.totalEnemiesDefeated++;
                }

                yield return new WaitForSeconds(spawnInterval); // Menunggu sebelum spawn musuh berikutnya
            }
        }
    }

    public void OnDeath()
    {
        Debug.Log("Musuh Dikalahkan");
        totalKill++; // Menambahkan total kill
        totalKillWave++; // Menambahkan kill dalam gelombang saat ini
        // Memeriksa apakah kill dalam gelombang sudah mencapai batas untuk meningkatkan jumlah spawn
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            Debug.Log("Jumlah spawn meningkat");
            totalKillWave = 0; // Reset counter kill gelombang
            defaultSpawnCount *= spawnCountMultiplier; // Meningkatkan jumlah spawn dasar
            spawnCount = defaultSpawnCount; // Memperbarui jumlah spawn untuk spawner

            // Menambahkan multiplier spawn untuk skala, dengan batas maksimum
            if (spawnCountMultiplier < 3)
            {
                spawnCountMultiplier += multiplierIncreaseCount;
            }
        }
    }
}
