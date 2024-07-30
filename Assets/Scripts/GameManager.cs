using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject deathEffect;

    private Vector3 respawnPosition;

    public int currentCoins;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerControler.Instance.transform.position;

        AddCoins(0);
    }

    // Update is called once per frame
    void Update() { if (Input.GetKeyDown(KeyCode.Escape)) { pauseUnpause(); } }

    public void Respawn()
    {
        StartCoroutine("RespawnCo");
        HealthManager.instance.PlayerKilled();
    }

    public IEnumerator RespawnCo()
    {
        PlayerControler.Instance.gameObject.SetActive(false);

        CameraControler.instance.brain.enabled = false;

        GUIManager.Instance.fadeOut = true;

        Instantiate(deathEffect,PlayerControler.Instance.transform.position + new Vector3(0f,1f,0f), PlayerControler.Instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        PlayerControler.Instance.transform.position = respawnPosition;
        PlayerControler.Instance.gameObject.SetActive(true);

        CameraControler.instance.brain.enabled = true;
        
        GUIManager.Instance.fadeIn = true;

        HealthManager.instance.ResetHealth();
    }

    public void setSpawnPoint(Vector3 spawnPoint)
    {
        respawnPosition = spawnPoint;
        Debug.Log("¡CHECK POINT!");
    }

    public void AddCoins(int coinsTooAdd)
    {
        currentCoins += coinsTooAdd;
        GUIManager.Instance.coinText.text = "" + currentCoins;
    }
    public void pauseUnpause()
    {
        if (GUIManager.Instance.pauseScreen.activeInHierarchy)
        {
            GUIManager.Instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            GUIManager.Instance.pauseScreen.SetActive(true);
            GUIManager.Instance.closeOptions();
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None  ;
        }
    }
}
