using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 stopFriction;
    
    public Vector2 maxSpeed = new Vector2(7f, 5f);
    public Vector2 timeToFullSpeed = new Vector2(1f, 1f);
    public Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    public Vector2 stopClamp = new Vector2(2.5f, 2.5f);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = new Vector2(maxSpeed.x / timeToFullSpeed.x, maxSpeed.y / timeToFullSpeed.y);
        stopFriction = new Vector2(moveVelocity.x / timeToStop.x, moveVelocity.y / timeToStop.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        Vector2 targetVelocity = new Vector2
        (moveDirection.x * maxSpeed.x, moveDirection.y * maxSpeed.y);

        Vector2 currentVelocity = rb.velocity;
        currentVelocity.x = Mathf.MoveTowards(currentVelocity.x, targetVelocity.x, stopFriction.x * Time.fixedDeltaTime);
        currentVelocity.y = Mathf.MoveTowards(currentVelocity.y, targetVelocity.y, stopFriction.y * Time.fixedDeltaTime);
        rb.velocity = currentVelocity;
    }

    public bool IsMoving()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > stopClamp.x || Mathf.Abs(rb.velocity.y) > stopClamp.y;
        Debug.Log("IsMoving: " + isMoving); 
        return isMoving;
    }
}