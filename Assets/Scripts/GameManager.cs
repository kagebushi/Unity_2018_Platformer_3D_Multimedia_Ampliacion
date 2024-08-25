using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject deathEffect;

    private Vector3 respawnPosition;

    public int currentCoins;

    public int levelEndMusic;

    public string levelToLoad;
    
    internal bool isRespawning;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerControler.Instance.transform.position;

        AddCoins(0);
    }

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

        isRespawning = true;

        Instantiate(deathEffect,PlayerControler.Instance.transform.position + new Vector3(0f,1f,0f), PlayerControler.Instance.transform.rotation);

        yield return new WaitForSeconds(2f);

        PlayerControler.Instance.transform.position = respawnPosition;
        PlayerControler.Instance.gameObject.SetActive(true);

        CameraControler.instance.brain.enabled = true;
        
        GUIManager.Instance.fadeIn = true;

        HealthManager.instance.ResetHealth();

        isRespawning = false;
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

    public IEnumerator LevelEndWater()
    {
        AudioManager.instance.StopMusic(AudioManager.instance.levelMusicToPlay);
        
        AudioManager.instance.PlayMusic(levelEndMusic);
        PlayerControler.Instance.stopMoving = true;
        yield return new WaitForSeconds(4f);

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_coins"))
        {
            if (currentCoins > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_coins")){
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins",currentCoins);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_coins", currentCoins);
        }

        SceneManager.LoadScene(levelToLoad);
    }
}
