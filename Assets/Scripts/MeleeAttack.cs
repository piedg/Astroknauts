using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    Transform attackPoint;
    [SerializeField] float attackRange;
    [SerializeField] bool isAttacking = true;
    [SerializeField] float attackRate = 2f;
    [SerializeField] bool isEnemy;

    AnimationsManager animator;
    DamageDealer damageDealer;
    AudioPlayer audioPlayer;
    Coroutine attackingCoroutine;


    void Awake()
    {
        attackPoint = GetComponentInChildren<Transform>();
        damageDealer = GetComponent<DamageDealer>();
        animator = GetComponent<AnimationsManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Update()
    {
        HandleAttack();
    }

    void HandleAttack()
    {
        if (isAttacking && attackingCoroutine == null)
        {
            attackingCoroutine = StartCoroutine(Attack());
        }
        else if (!isAttacking && attackingCoroutine != null)
        {
            StopCoroutine(Attack());
            attackingCoroutine = null;
        }
    }

    IEnumerator Attack()
    {
        while (true)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, LayerMask.GetMask("Player"));

            if (hit != null)
            {
                isAttacking = true;

                if (!hit.GetComponent<Player>().PlayerIsAlive())
                {
                    isAttacking = false;
                }

                if (isEnemy && isAttacking)
                {
                    hit.GetComponent<Health>().DisplayHitText(hit);
                    audioPlayer.BlobAttackClip();
                }
                hit.GetComponent<Health>().TakeDamage(damageDealer.GetDamage());
            }

            else
            {
                isAttacking = false;
            }
            animator.HandleAttack(isAttacking);

            yield return new WaitForSeconds(attackRate);


        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }

    }
}
