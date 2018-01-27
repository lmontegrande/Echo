using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public void Start()
    {
        HideEchoTargets();
        HideEnemies();
    }

    public void HideEchoTargets()
    {
        GameObject[] echoTargets = GameObject.FindGameObjectsWithTag("EchoTarget");
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
}
