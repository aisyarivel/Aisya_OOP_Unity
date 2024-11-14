using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 2f;
    private Vector2 moveDirection;
    private SpriteRenderer spriteRenderer;

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
        float spawnX = Random.Range(0, 2) == 0 ? Screen.width / 120f : -Screen.width / 110f;
        float spawnY = Random.Range(-Screen.height / 80f, Screen.height / 80f);

        transform.position = new Vector2(spawnX, spawnY);
        moveDirection = spawnX < 0 ? Vector2.right : Vector2.left;

        spriteRenderer.flipX = moveDirection == Vector2.left;
        transform.rotation = Quaternion.identity;
    }

    private void Start()
    {
        RespawnAtSide();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
