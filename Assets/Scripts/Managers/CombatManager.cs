using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public GameStats gameStats;  // Array of spawners
    public float timer = 0; // Timer untuk melacak interval wave
    [SerializeField] private float waveInterval = 5f; // Waktu antara setiap wave
    public int waveNumber = 1; // Nomor wave saat ini
    public int totalEnemiesDefeated = 0;
    private int totalEnemiesInCurrentWave = 0; // Total musuh yang akan spawn pada wave ini
    private int totalPoints = 0; // Total musuh yang dikalahkan sepanjang semua wave
    private int totalEnemiesLeftInWave = 0; 

    private void Start()
    {
        waveNumber = 0;
        // Menghubungkan CombatManager ke setiap enemy spawner
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.combatManager = this;
        }

        // Memperbarui informasi UI untuk wave dan musuh yang tersisa
        gameStats.UpdateWave(waveNumber);
        gameStats.UpdateEnemiesLeft(totalEnemiesDefeated);
        gameStats.UpdatePoints(totalPoints);
    }

    private void Update()
    {
        if (AllSpawnersFinished())
        {
            timer += Time.deltaTime;

            // Jika waktu untuk wave berikutnya telah tercapai
            if (timer >= waveInterval)
            {
                ProceedToNextWave();
                timer = 0;
            }
        }
    }

    private void ProceedToNextWave()
    {
        timer = 0;
        waveNumber++;

        // Reset jumlah musuh untuk wave berikutnya
        totalEnemiesInCurrentWave = 0;
        totalEnemiesLeftInWave = 0;

        // Menjumlahkan total musuh yang akan muncul pada wave ini
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                totalEnemiesInCurrentWave += spawner.spawnCount;
            }
        }

        // Memperbarui UI untuk wave dan jumlah musuh yang tersisa
        gameStats.UpdateWave(waveNumber);
        gameStats.UpdateEnemiesLeft(totalEnemiesInCurrentWave);

        // Memulai spawning untuk setiap enemy spawner
        foreach (var enemySpawner in enemySpawners)
        {
            if (enemySpawner != null)
            {
                enemySpawner.StartSpawning();
            }
        }
    }

    private bool AllSpawnersFinished()
    {
        // Mengecek apakah semua spawner telah selesai
        foreach (var spawner in enemySpawners)
        {
            if (spawner != null && spawner.isSpawning)
            {
                return false; // Jika ada spawner yang masih aktif, wave belum selesai
            }
        }
        return true; // Semua spawner telah selesai spawning
    }

    public void RegisterKill(int enemyLevel)
    {
        totalEnemiesDefeated++;
        totalPoints += enemyLevel; // Menambahkan poin berdasarkan level musuh
        // Mengupdate UI dengan jumlah total poin
        Debug.Log($"Total musuh yang dikalahkan: {totalEnemiesDefeated}");
        gameStats.UpdatePoints(totalPoints);
    }
}
