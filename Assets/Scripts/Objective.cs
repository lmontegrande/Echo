using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    public GameObject loadOnWinGameObjects;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            loadOnWinGameObjects.SetActive(true);
            gameObject.SetActive(false);
            foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("EnemyContainer"))
            {
                enemy.GetComponent<Enemy>().OnEcho();
            }
        }
    }
}
