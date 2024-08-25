using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossController.instance.DamageBoss();
            PlayerControler.Instance.Bounce();
        }
    }
}
