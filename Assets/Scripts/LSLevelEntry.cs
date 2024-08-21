using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSLevelEntry : MonoBehaviour
{
    public bool canLoadLevel;
    public string LevelName, levelToCheck;
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
        }
        
    }

    void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            canLoadLevel = false;
        }
    }

    public IEnumerator LevelLoadWaiter()
    {
        PlayerControler.Instance.stopMoving = true;
        GUIManager.Instance.fadeOut = true;

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(LevelName);
        PlayerControler.Instance.stopMoving = false;
    }
}
