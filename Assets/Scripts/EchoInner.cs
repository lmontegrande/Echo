using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoInner : MonoBehaviour {
    public delegate void OnCollisionHandle(Collider other);
    public OnCollisionHandle OnEchoCollision;

    private bool hasHit = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EchoTarget" && OnEchoCollision != null && !hasHit)
        {
            hasHit = true;
            OnEchoCollision.Invoke(other);
        }

        this.enabled = false;
    }
}