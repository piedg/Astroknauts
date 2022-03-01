using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    Vector2 moveInput;

    Rigidbody2D rb;
    AnimationsManager animations;
    Shooter playerShooting;
    Health health;

    [HideInInspector]
    public Vector2 lookDirection = new Vector2(0, -1);
    public Vector2 lastLookDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animations = GetComponent<AnimationsManager>();
        health = GetComponent<Health>();
        playerShooting = GetComponent<Shooter>();
    }

    void FixedUpdate()
    {
        HandleMove();
        IsDead();
        animations.HandleMovement(moveInput.x, moveInput.y);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void HandleMove()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * movementSpeed, moveInput.y * movementSpeed);
        rb.velocity = playerVelocity;

        if (!Mathf.Approximately(moveInput.x, 0.0f) || !Mathf.Approximately(moveInput.y, 0.0f))
        {
            lookDirection.Set(moveInput.x, moveInput.y);
            lookDirection.Normalize();
        }
    }

    void OnFire(InputValue value)
    {
        if (value != null)
        {
            playerShooting.isShooting = value.isPressed;
        }
    }

    void IsDead()
    {
        if (!health.isAlive)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            animations.HandleDie(health.isAlive);
        }
        return;
    }

    public bool PlayerIsAlive()
    {
        return health.isAlive;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

}
