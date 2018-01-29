using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour {

    public static ExitManager instance;

    public void Awake()
    {
     if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
    }
}
