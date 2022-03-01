using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pickups : MonoBehaviour
{
    float countDown = 10f;
    float defaultFiringRate;
    float defaultMovementSpeed;
    Health playerHealth;

    [SerializeField] float powerUpValue;
    public bool isActive;

    void Awake()
    {
        defaultFiringRate = FindObjectOfType<Player>().GetComponent<Shooter>().GetFiringRate();
        defaultMovementSpeed = FindObjectOfType<Player>().GetComponent<Player>().GetMovementSpeed();
        playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
    }

    void Start()
    {
        StartCoroutine(DestroyCountDown());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (gameObject.tag == "FiringRatePowerUp")
            {
                StartCoroutine(LowFiringRate(other));
            }
            else if (gameObject.tag == "MoveSpeedPowerUp")
            {
                StartCoroutine(MovementSpeedUp(other));
            }
            else if (gameObject.tag == "Medikit")
            {
                Medikit();
            }
        }
    }

    IEnumerator LowFiringRate(Collider2D other)
    {
        HidePowerUp();
        other.GetComponent<Shooter>().firingRate = powerUpValue;

        yield return new WaitForSeconds(countDown);
        other.GetComponent<Shooter>().firingRate = defaultFiringRate;

        Destroy(gameObject);
    }

    IEnumerator MovementSpeedUp(Collider2D other)
    {
        HidePowerUp();
        other.GetComponent<Player>().movementSpeed = powerUpValue;

        yield return new WaitForSeconds(countDown);
        other.GetComponent<Player>().movementSpeed = defaultMovementSpeed;
        Destroy(gameObject);
    }

    void Medikit()
    {
        if (playerHealth.health < 3)
        {
            playerHealth.health += (int)powerUpValue;
            Destroy(gameObject);
        }
    }

    void HidePowerUp()
    {
        foreach (SpriteRenderer children in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            children.enabled = false;
        }

        foreach (Collider2D children in gameObject.GetComponentsInChildren<Collider2D>())
        {
            children.enabled = false;
        }
    }

    IEnumerator DestroyCountDown()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
}
