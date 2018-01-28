using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject echoPrefab;

	public void Update()
    {
        HandleInput();
    }

    public void HandleInput()
    {
        if(Input.GetButtonDown("Fire1")) {
            GameObject echoInstance = Instantiate(echoPrefab, transform.position, transform.rotation);
            echoInstance.tag = "echo";
            echoInstance.GetComponent<Echo>().Launch();
        }
    }
}
