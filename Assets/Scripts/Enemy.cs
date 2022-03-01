using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxDistanceToPlayer;
    float distanceBetweenPlayer;
    Vector2 movement;

    Health health;
    AnimationsManager animations;
    Rigidbody2D rb;
    public bool isSkra;

    [HideInInspector]
    public Vector2 lookDirection;

    void Awake()
    {
        playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animations = GetComponent<AnimationsManager>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        PlayerPosition();
    }

    void FixedUpdate()
    {
        //Prendo la distanza tra il nemico ed il giocatore
        distanceBetweenPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (!health.isAlive)
        {
            return;
        }
        Move(movement);
        animations.HandleMovement(movement.x, movement.y);
    }

    void Move(Vector2 direction)
    {
        //Se la distanza tra il giocatore e il mostro è maggiore di una distanza massima allora avvicina il nemico

        if (distanceBetweenPlayer > maxDistanceToPlayer)
        {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        }

        if (!Mathf.Approximately(direction.x, 0.0f) || !Mathf.Approximately(direction.y, 0.0f))
        {
            lookDirection.Set(direction.x, direction.y);
            lookDirection.Normalize();
        }
    }

    void PlayerPosition()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
    }
}
