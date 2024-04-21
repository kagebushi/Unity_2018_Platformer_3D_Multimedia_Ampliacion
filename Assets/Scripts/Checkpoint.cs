using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject cpON, cpOFF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.setSpawnPoint(transform.position);

            Checkpoint[] allCheckpoint = FindObjectsOfType<Checkpoint>();
            for (int i = 0; i < allCheckpoint.Length; i++)
            {
                if (allCheckpoint[i] != this)
                {
                    allCheckpoint[i].cpOFF.SetActive(true);
                    allCheckpoint[i].cpON.SetActive(false);
                }
            }

            cpOFF.SetActive(false);
            cpON.SetActive(true);
        }
    }
}
