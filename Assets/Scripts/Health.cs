using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [Header("General")]
    public int health = 50;

    public bool isAlive;

    [Header("Player Status")]
    [SerializeField] bool isPlayer;
    [SerializeField] float timeInvincible = 2.0f;
    public bool isInvincible;
    float invincibleTimer;

    [Header("Hit Text")]
    [SerializeField] TextMeshPro hitText;
    [SerializeField] TextMeshPro invulnerable;

    Collider2D coll2D;
    AnimationsManager animator;
    AudioPlayer audioPlayer;
    Enemy enemyType;

    void Awake()
    {
        isAlive = true;
        animator = GetComponent<AnimationsManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        coll2D = GetComponent<Collider2D>();
        enemyType = GetComponent<Enemy>();
    }

    void Update()
    {
        if (isPlayer)
        {
            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer < 0)
                    isInvincible = false;
            }
        }
    }

    public void TakeDamage(int value)
    {

        if (isPlayer)
        {
            if (isInvincible) return;
            audioPlayer.DamageClip();

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        health -= value;

        if (health <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            if (other.tag == "Bullet")
            {
                audioPlayer.BulletImpactClip();
                Destroy(other.gameObject);
            }
            if (other.tag == "EnemyBullet")
            {
                audioPlayer.BulletImpactClip();
                Destroy(other.gameObject);
            }
            DisplayHitText(other);
            TakeDamage(damageDealer.GetDamage());

        }
    }

    void Die()
    {
        coll2D.enabled = false;
        isAlive = false;
        animator.HandleDie(isAlive);
        if (isPlayer) return;
        Destroy(gameObject, 1f);

        if (enemyType.isSkra)
        {
            audioPlayer.SkraDieClip();
        }
        else
        {
            audioPlayer.BlobDieClip();
        }
    }

    public void DisplayHitText(Collider2D other)
    {
        if (hitText != null && !isInvincible)
        {
            TextMeshPro instance = Instantiate(hitText, other.GetComponent<Rigidbody2D>().position + Vector2.up * 0.5f, transform.rotation, transform);
            Destroy(instance.gameObject, 2f);
        }
        else if (invulnerable != null && isInvincible)
        {
            TextMeshPro instance = Instantiate(invulnerable, other.GetComponent<Rigidbody2D>().position + Vector2.up * 0.5f, transform.rotation, transform);
            Destroy(instance.gameObject, 2f);
        }
    }
}
