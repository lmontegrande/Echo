using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    public GameObject victory;
    public GameObject player;
    private PlayerController character;

	// Use this for initialization
	void Start () {
        victory.SetActive(false);
        character = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Victory is triggered");
            victory.SetActive(true);
            character.speed = 0;
            character.turnSpeed = 0;
            character.strafeSpeed = 0;
        }
    }
}
