using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;
    public Image blackScreen;

    public float fadespeed;

    public bool fadeOut, fadeIn;

    public Text healthText;
    public Image healthImage;
    
    public Text coinText;

    public GameObject pauseScreen;

    public GameObject optionPanel;

    public Slider musicVolSlider, sfxVolSlider;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            blackScreen.color = new Color(blackScreen.color.r,blackScreen.color.g,blackScreen.color.b,Mathf.MoveTowards(blackScreen.color.a,1f,fadespeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                fadeOut = false;
            }
        }

        if (fadeIn)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadespeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                fadeIn = false;
            }
        }
    }

    public void Resume()
    {
        GameManager.instance.pauseUnpause();
    }

    public void openOptions()
    {
        optionPanel.SetActive(true);
    }

    public void closeOptions()
    {
        optionPanel.SetActive(false);
    }

    public void GoToLevelSelect()
    {

    }

    public void GoToMainMenu()
    {

    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }
}
