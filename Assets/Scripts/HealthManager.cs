using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currenthealth, maxhealth;

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
        
    }

    public void Hurt()
    {
        currenthealth--;

        if (currenthealth <= 0 )
        {
            currenthealth = 0;
            GameManager.instance.Respawn();
        }

        Debug.Log("Health left: " + currenthealth);
    }
}
