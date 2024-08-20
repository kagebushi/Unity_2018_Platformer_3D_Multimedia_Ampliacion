using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstLevel;

    public string selectLevel;

    public GameObject continueButton;

    public void Start()
    {
        if (PlayerPrefs.HasKey("continue"))
        {
            continueButton.SetActive(true);
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("continue", 0);
    }
    
    public void Continue()
    {
        SceneManager.LoadScene(selectLevel);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
