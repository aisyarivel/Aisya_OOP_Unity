using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 2f;
    private Vector2 moveDirection;
    public GameObject enemyPrefab;

    private void Start()
    {
        RespawnAtSide();
        SpawnMultipleEnemies(enemyPrefab, Random.Range(3, 7));
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (transform.position.x < -Screen.width / 80f || transform.position.x > Screen.width / 80f)
        {
            RespawnAtSide();
        }
    }

    private void RespawnAtSide()
    {
        float spawnX = Random.Range(0, 2) == 0 ? -Screen.width / 110f : Screen.width / 120f;
        float spawnY = Random.Range(-Screen.height / 80f, Screen.height / 80f);

        transform.position = new Vector2(spawnX, spawnY);
        moveDirection = spawnX < 0 ? Vector2.right : Vector2.left;
        transform.rotation = Quaternion.identity;
    }

    public static void SpawnMultipleEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            EnemyHorizontal enemyScript = newEnemy.GetComponent<EnemyHorizontal>();
            if (enemyScript != null)
            {
                enemyScript.RespawnAtSide();
            }
        }
    }
}
