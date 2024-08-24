using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIManager : MonoBehaviour
{
    public static LSUIManager instance;

    public Text lnameText;

    public Text coinText;

    public GameObject lnamePanel;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
