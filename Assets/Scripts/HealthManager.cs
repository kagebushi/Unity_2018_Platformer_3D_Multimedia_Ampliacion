using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currenthealth, maxhealth;

    public float invincibleLength = 2f;
    private float invinvibleCounter;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (invinvibleCounter > 0)
        {
            invinvibleCounter -= Time.deltaTime;

            for (int i = 0; i < PlayerControler.Instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(invinvibleCounter * 5f) % 2 == 0)
                {
                    PlayerControler.Instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerControler.Instance.playerPieces[i].SetActive(false);
                }
            
                if (invinvibleCounter <= 0)
                {
                    PlayerControler.Instance.playerPieces[i].SetActive(true);
                }

            }

        }
    }

    public void Hurt()
    {
        if (invinvibleCounter <= 0)
        {

            currenthealth--;

            if (currenthealth <= 0)
            {
                currenthealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerControler.Instance.knockback();
                invinvibleCounter = invincibleLength;
            }

            Debug.Log("Health left: " + currenthealth);
        }
    }

    public void ResetHealth()
    {
        currenthealth = maxhealth;
    }

    public void AddHealth(int amountToHeal)
    {
        currenthealth += amountToHeal;
        if (currenthealth > maxhealth)
        {
            currenthealth = maxhealth;
        }
    }
}
