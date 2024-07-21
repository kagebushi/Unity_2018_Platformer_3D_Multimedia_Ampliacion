using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healAmmount;
    public bool isFullHeal;

    public GameObject HealEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(HealEffect, PlayerControler.Instance.transform.position + new Vector3(0f, 1f, 0f), PlayerControler.Instance.transform.rotation);
            Destroy(gameObject);

            if (isFullHeal)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                HealthManager.instance.AddHealth(healAmmount);
            }
        }
    }
}
