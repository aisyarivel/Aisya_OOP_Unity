using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyClickSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyVariants;
    [SerializeField] private int selectedVariant = 0;

    private void Start()
    {
        Assert.IsTrue(enemyVariants.Length > 0, "Tambahkan setidaknya 1 Prefab Enemy terlebih dahulu!");
    }

    private void Update()
    {
        // Memeriksa input angka untuk memilih varian musuh
        for (int i = 1; i <= enemyVariants.Length; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                selectedVariant = i - 1;
            }
        }

        // Jika tombol kanan mouse ditekan, panggil fungsi SpawnEnemy
        if (Input.GetMouseButtonDown(1))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Memastikan indeks varian valid sebelum membuat instance musuh
        if (selectedVariant < enemyVariants.Length)
        {
            Instantiate(enemyVariants[selectedVariant]);
        }
    }
}
