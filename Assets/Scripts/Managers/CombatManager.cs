using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners; // Array untuk daftar spawner musuh
    public float timer = 0; // Timer untuk melacak interval antar gelombang
    [SerializeField] private float waveInterval = 5f; // Waktu jeda antar gelombang
    public int waveNumber = 1; // Nomor gelombang saat ini
    public int totalEnemiesDefeated = 0; // Total musuh yang dikalahkan di semua gelombang

    private void Start()
    {
        waveNumber = 0;
        // Menautkan setiap spawner ke CombatManager ini
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.combatManager = this;
        }
    }

    private void Update()
    {
        // Jika semua spawner telah selesai, mulai hitung waktu jeda untuk gelombang berikutnya
        if (AllSpawnersFinished())
        {
            timer += Time.deltaTime;

            // Jika waktu jeda sudah cukup, mulai gelombang berikutnya
            if (timer >= waveInterval)
            {
                StartNextWave();
                timer = 0;
            }
        }
    }

    private void StartWave()
    {
        // Mengatur ulang spawner untuk memulai gelombang baru
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.defaultSpawnCount = waveNumber; // Mengatur jumlah spawn berdasarkan nomor gelombang
                spawner.spawnCountMultiplier = 1; // Reset multiplier untuk gelombang baru
                spawner.isSpawning = true; // Aktifkan proses spawning
            }
        }
    }

    private void StartNextWave()
    {
        timer = 0;
        waveNumber++; // Tingkatkan nomor gelombang
        totalEnemiesDefeated = 0; // Reset jumlah musuh yang dikalahkan untuk gelombang baru

        // Mengatur ulang setiap spawner untuk gelombang baru
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            if (enemySpawner != null)
            {
                enemySpawner.defaultSpawnCount = waveNumber; // Sesuaikan jumlah spawn untuk gelombang baru
                enemySpawner.spawnCountMultiplier = waveNumber; // Tingkatkan tingkat kesulitan atau jumlah spawn
                enemySpawner.isSpawning = true; // Mulai ulang proses spawning
            }
        }
    }

    private bool AllSpawnersFinished()
    {
        // Memeriksa apakah semua spawner sudah selesai
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null && spawner.isSpawning)
            {
                return false; // Jika ada spawner yang masih aktif, gelombang belum selesai
            }
        }
        return true; // Semua spawner telah selesai
    }

    public void RegisterKill()
    {
        totalEnemiesDefeated++; // Tambahkan jumlah total musuh yang dikalahkan
        // Opsional: Logika tambahan untuk menangani efek global dari kill
        Debug.Log($"Total musuh yang dikalahkan: {totalEnemiesDefeated}");
    }
}
