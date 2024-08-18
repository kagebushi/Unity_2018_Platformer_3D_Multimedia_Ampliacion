using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int maxHealth = 1;
    private int currentHealth;

    public int deathSound = 6;

    public GameObject deathEffect, dropItem;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            AudioManager.instance.PlaySFX(deathSound);
            Destroy(gameObject);

            Instantiate(deathEffect, transform.position, transform.rotation);
            Instantiate(dropItem, transform.position, transform.rotation);
        }
        PlayerControler.Instance.Bounce();
    }
}
