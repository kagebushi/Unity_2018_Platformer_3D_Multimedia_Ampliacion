using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstLevel;

    public string selectLevel;

    public GameObject continueButton;

    public string[] levelNames;

    public void Start()
    {
        if (PlayerPrefs.HasKey("continue"))
        {
            continueButton.SetActive(true);
        }
    }

    public void NewGame()
    {
        ResetData();
        SceneManager.LoadScene(firstLevel);

        PlayerPrefs.SetInt("continue", 0);
        PlayerPrefs.SetString("CurrentLevel",firstLevel);
    }
    
    public void Continue()
    {
        SceneManager.LoadScene(selectLevel);

    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ResetData()
    {
        for (int i = 0; i < levelNames.Length; i++) 
        {
            PlayerPrefs.SetInt(levelNames[i] + "_unlocked", 0);
        }
    }
}
