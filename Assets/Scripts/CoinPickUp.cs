using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public int value;

    public GameObject coinEffect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddCoins(value);
            AudioManager.instance.PlaySFX(5);
            Destroy(gameObject);
            Instantiate(coinEffect,transform.position,transform.rotation);
        }
    }
}
