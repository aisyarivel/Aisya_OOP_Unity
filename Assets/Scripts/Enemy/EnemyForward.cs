using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 2f;
    public GameObject enemyPrefab;

    private void Start()
    {
        RespawnAtTop();
        SpawnMultipleEnemies(enemyPrefab, Random.Range(3, 7));
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < -Screen.height / 85f)
        {
            RespawnAtTop();
        }
    }

    private void RespawnAtTop()
    {
        float randomX = Random.Range(-Screen.width / 105f, Screen.width / 105f);
        transform.position = new Vector2(randomX, Screen.height / 85f);
        transform.rotation = Quaternion.identity;
    }

    public static void SpawnMultipleEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            EnemyForward enemyScript = newEnemy.GetComponent<EnemyForward>();
            if (enemyScript != null)
            {
                enemyScript.RespawnAtTop();
            }
        }
    }
}
