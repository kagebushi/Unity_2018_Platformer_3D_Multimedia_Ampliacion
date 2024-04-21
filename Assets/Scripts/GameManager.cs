using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        respawnPosition = PlayerControler.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine("RespawnCo");
    }

    public IEnumerator RespawnCo()
    {
        PlayerControler.Instance.gameObject.SetActive(false);

        CameraControler.instance.brain.enabled = false;

        GUIManager.Instance.fadeOut = true;

        yield return new WaitForSeconds(2f);

        PlayerControler.Instance.transform.position = respawnPosition;
        PlayerControler.Instance.gameObject.SetActive(true);

        CameraControler.instance.brain.enabled = true;
        
        GUIManager.Instance.fadeIn = true;
    }

    public void setSpawnPoint(Vector3 spawnPoint)
    {
        respawnPosition = spawnPoint;
        Debug.Log("¡CHECK POINT!");
    }

}
