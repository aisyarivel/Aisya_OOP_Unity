using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform playerTransform;
    private float speed = 2.0f;
    public GameObject enemyPrefab;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in the scene. EnemyTargeting will not move.");
        }

        SpawnMultipleEnemies(enemyPrefab, Random.Range(1, 6));
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void SpawnMultipleEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            float spawnX = Random.Range(0, 2) == 0 ? -Screen.width / 110f : Screen.width / 110f;
            float spawnY = Random.Range(-Screen.height / 80f, Screen.height / 80f);

            GameObject newEnemy = Instantiate(enemyPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
            EnemyTargeting enemyScript = newEnemy.GetComponent<EnemyTargeting>();
            
            if (enemyScript != null)
            {
                enemyScript.playerTransform = playerTransform;
            }
        }
    }
}
