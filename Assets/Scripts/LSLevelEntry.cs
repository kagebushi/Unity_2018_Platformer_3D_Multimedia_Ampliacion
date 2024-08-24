using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    public bool canLoadLevel;
    public string LevelName, levelToCheck, displayName;
    public GameObject MapPointActive, MapPointIncative;
    private bool _levelUnlocked, _levelLoading;

    void Start()
    {

        if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1 || levelToCheck == string.Empty)
        {
            MapPointActive.SetActive(true);
            MapPointIncative.SetActive(false);
            _levelUnlocked = true;
        }
        else
        {
            MapPointActive.SetActive(false);
            MapPointIncative.SetActive(true);
            _levelUnlocked = false;
        }

        if (PlayerPrefs.GetString("CurrentLevel") == LevelName)
        {
            PlayerControler.Instance.transform.position = transform.position;
            LevelSelectResetPlayer.instance.respawPosition = transform.position;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && canLoadLevel && _levelUnlocked && !_levelLoading)
        {
            StartCoroutine("LevelLoadWaiter");
            _levelLoading = true;
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canLoadLevel = true;
            
            LSUIManager.instance.lnamePanel.SetActive(true);
            LSUIManager.instance.lnameText.text = displayName;
            
            if (PlayerPrefs.HasKey(LevelName + "_coins"))
            {
                LSUIManager.instance.coinText.text = PlayerPrefs.GetInt(LevelName + "_coins").ToString();
            }
            else {
                LSUIManager.instance.coinText.text = "???";
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            canLoadLevel = false;
            
            LSUIManager.instance.lnamePanel.SetActive(false);
        }
    }

    public IEnumerator LevelLoadWaiter()
    {
        PlayerControler.Instance.stopMoving = true;
        GUIManager.Instance.fadeOut = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(LevelName);
        PlayerControler.Instance.stopMoving = false;
        PlayerPrefs.SetString("CurrentLevel", LevelName);
    }
}
