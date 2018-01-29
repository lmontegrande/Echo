using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public string introSceneString;

    public void Start()
    {
        HideEchoTargets();
        HideEnemies();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HideEchoTargets()
    {
        GameObject[] echoTargets = GameObject.FindGameObjectsWithTag("EchoCube");
        foreach(GameObject echoTarget in echoTargets)
        {
            echoTarget.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void HideEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void LoadIntro()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(introSceneString);
    }
}
