using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform weapon;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    public float firingRate = 0.5f;
    float nextFire = 0f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 0.2f;

    [HideInInspector]
    public bool isShooting;
    bool playerIsAlive;

    Coroutine firingCoroutine;
    AnimationsManager animator;
    Player player;
    Enemy enemy;
    Health health;
    AudioPlayer audioPlayer;

    void Awake()
    {
        animator = GetComponent<AnimationsManager>();
        player = FindObjectOfType<Player>();
        enemy = GetComponent<Enemy>();
        health = GetComponent<Health>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isShooting = true;
        }
    }

    void Update()
    {
        HandleFire();
    }

    public void HandleFire()
    {
        animator.HandleAttack(isShooting);

        if (isShooting && firingCoroutine == null && Time.time > nextFire)
        {
            nextFire = Time.time + firingRate;
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isShooting && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (player.PlayerIsAlive())
        {
            GameObject instance;
            if (useAI && health.isAlive)
            {
                instance = Instantiate(projectilePrefab, weapon.position, Quaternion.identity);
            }
            else
            {
                instance = Instantiate(projectilePrefab, gameObject.GetComponent<Rigidbody2D>().position + new Vector2(0.2f, -0.2f), Quaternion.identity);
            }

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                if (useAI)
                {
                    audioPlayer.SkraClip();
                    rb.velocity = enemy.lookDirection * projectileSpeed;
                }
                else
                {
                    audioPlayer.PlayShootingClip();
                    rb.velocity = player.lookDirection * projectileSpeed;
                }
            }

            Destroy(instance, projectileLifetime);

            if (useAI)
            {
                yield return new WaitForSeconds(GetRandomSpawnTime());
            }
            else
            {
                yield return new WaitForSeconds(firingRate);
            }
        }
        animator.HandleAttack(health);
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(firingRate - spawnTimeVariance, firingRate + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }


    public float GetFiringRate()
    {
        return this.firingRate;
    }
}
