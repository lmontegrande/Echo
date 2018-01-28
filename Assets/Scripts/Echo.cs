using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour {

    public EchoInner echoInner;
    public Color ballColor = Color.yellow;
    public Color cubeColor = Color.white;
    public Color enemyColor = Color.red;
    public Color darknessColor = Color.black;
    public float speed;
    public float lifeTime;
    public float blinkTime;
    public float ballFadeOutTimer;

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
        StartCoroutine(Blink(echoInner.GetComponent<MeshRenderer>(), ballColor, Color.clear, ballFadeOutTimer));
        StartCoroutine(DeathCountdown());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EchoCube")
            other.GetComponent<EchoCube>().Blink(cubeColor, darknessColor, blinkTime);
        if (other.tag == "Enemy")
            other.GetComponent<EchoCube>().Blink(enemyColor, darknessColor, blinkTime);
    }

    private void EchoInnerOnCollision(Collider other)
    {
        // Todo
        //lifeTimer = 0;
        //Vector3 reflectDirection = Vector3.Reflect(_rigidbody.velocity, other.contacts[0].normal).normalized;
        //_rigidbody.velocity = Vector3.ProjectOnPlane(reflectDirection, Vector3.up) * speed;
    }

    private IEnumerator Blink(MeshRenderer blinkTarget, Color startColor, Color endColor, float time)
    {
        blinkTarget.enabled = true;
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            blinkTarget.material.color = Color.Lerp(startColor, endColor, timer / time);
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
