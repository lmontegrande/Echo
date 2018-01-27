using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour {

    public EchoInner echoInner;
    public Color ballColor = Color.yellow;
    public Color tileColor = Color.white;
    public Color enemyColor = Color.red;
    public Color darknessColor = Color.black;
    public float speed;
    public float lifeTime;
    public float blinkTime;
    public float ballFadeOut;

    private Rigidbody _rigidbody;
    private float lifeTimer = 0f;

    public void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        echoInner.OnEchoCollision = EchoInnerOnCollision; 
    }

    public void Launch()
    {
        _rigidbody.velocity = transform.forward * speed;
        StartCoroutine(Blink(echoInner.GetComponent<MeshRenderer>(), ballColor));
        StartCoroutine(DeathCountdown());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EchoTarget")
            StartCoroutine(Blink(other.GetComponent<MeshRenderer>(), tileColor));
        if (other.tag == "Enemy")
            StartCoroutine(Blink(other.GetComponent<MeshRenderer>(), enemyColor));
    }

    private void EchoInnerOnCollision(Collider other)
    {
        // Todo
        //lifeTimer = 0;
        //Vector3 reflectDirection = Vector3.Reflect(_rigidbody.velocity, other.contacts[0].normal).normalized;
        //_rigidbody.velocity = Vector3.ProjectOnPlane(reflectDirection, Vector3.up) * speed;
    }

    private IEnumerator Blink(MeshRenderer blinkTarget, Color blinkColor)
    {
        blinkTarget.enabled = true;
        float blinkTimer = 0f;
        while (blinkTimer < blinkTime)
        {
            blinkTimer += Time.deltaTime;
            blinkTarget.material.color = Color.Lerp(blinkColor, darknessColor, blinkTimer / blinkTime);
            yield return null;
        }
        blinkTarget.enabled = false;
    }

    private IEnumerator DeathCountdown()
    {
        while (lifeTimer < lifeTime) {
            lifeTimer += Time.deltaTime;
            yield return null;
        }
        gameObject.GetComponent<SphereCollider>().enabled = false;
        Destroy(gameObject, blinkTime * 2);
    }
}
