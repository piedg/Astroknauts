using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    Health playerHealth;
    Pickups pickups;

    [Header("Health")]
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite heart;
    [SerializeField] Sprite brokenHeart;

    void Awake()
    {
        playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
    }

    void Update()
    {
        HandleHearts();
        HandlePowerUp();
    }

    void HandleHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.health)
            {
                hearts[i].sprite = heart;
            }
            else
            {
                hearts[i].sprite = brokenHeart;
            }
        }
    }

    void HandlePowerUp()
    {

    }
}
