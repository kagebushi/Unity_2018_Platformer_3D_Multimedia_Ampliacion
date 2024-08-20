using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectResetPlayer : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControler.Instance.gameObject.SetActive(false);
            PlayerControler.Instance.transform.position = new Vector3(0, 2, 0);
            PlayerControler.Instance.gameObject.SetActive(true);
        }
    }
}
