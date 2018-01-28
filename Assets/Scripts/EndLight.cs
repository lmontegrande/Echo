using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLight : MonoBehaviour {

    public GameObject endLight;

	// Use this for initialization
	void Start () {
        endLight.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            endLight.SetActive(true);
            Debug.Log("Light is active");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            endLight.SetActive(false);
            Debug.Log("Light goes out");
        }
    }
}
