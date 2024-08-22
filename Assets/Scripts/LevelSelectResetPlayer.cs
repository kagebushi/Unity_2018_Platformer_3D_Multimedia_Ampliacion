using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectResetPlayer : MonoBehaviour
{
    public static LevelSelectResetPlayer instance;

    public Vector3 respawPosition;

    private void Awake()
    {
        instance = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControler.Instance.gameObject.SetActive(false);
            PlayerControler.Instance.transform.position = respawPosition;
            PlayerControler.Instance.gameObject.SetActive(true);
        }
    }
}
