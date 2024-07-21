using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currenthealth, maxhealth;

    public float invincibleLength = 2f;
    private float invinvibleCounter;

    public Sprite[] healthBarImages;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
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

            //Debug.Log("Health left: " + currenthealth);

        }
        UpdateUI();
    }

    public void ResetHealth()
    {
        currenthealth = maxhealth;
        GUIManager.Instance.healthImage.enabled = true;
        UpdateUI();
    }

    public void AddHealth(int amountToHeal)
    {
        currenthealth += amountToHeal;
        if (currenthealth > maxhealth)
        {
            currenthealth = maxhealth;
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        GUIManager.Instance.healthText.text = currenthealth.ToString();

        switch (currenthealth)
        {
            case 5:
                GUIManager.Instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                GUIManager.Instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                GUIManager.Instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                GUIManager.Instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                GUIManager.Instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                GUIManager.Instance.healthImage.enabled = false;
                break;
        }
    }

    public void PlayerKilled()
    {
        currenthealth = 0;
        UpdateUI();
    }
}
