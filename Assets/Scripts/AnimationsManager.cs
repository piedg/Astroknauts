using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsManager : MonoBehaviour
{
    bool isWalking;
    bool isAttacking;

    Vector3 moveDir;
    Animator animator;
    Rigidbody2D rb;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void HandleMovement(float x, float y)
    {
        //Controlla se x e y sono diversi da 0 (quindi in movimento)
        // Invece di fare moveInput.x != 0 Approximately è più preciso con i float
        if (!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(y, 0.0f))
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);
            if (!isWalking)
            {
                isWalking = true;
                animator.SetBool("isMoving", isWalking);
            }
        }
        else if (isWalking)
        {
            isWalking = false;
            animator.SetBool("isMoving", isWalking);
            StopMoving();
        }
        moveDir = new Vector2(x, y).normalized;
    }

    void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void HandleDie(bool isAlive)
    {
        animator.SetBool("isDead", !isAlive);
    }

    public void HandleAttack(bool isAttacking)
    {
        animator.SetBool("isAttacking", isAttacking);
    }
}
