using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoCube : MonoBehaviour {

    public delegate void OnEchoHandle();
    public OnEchoHandle OnEcho;

    private Coroutine currentCoroutine;

    public void Blink(Color startColor, Color endColor, float time)
    {
        if (OnEcho != null)
            OnEcho.Invoke();
        
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(BlinkCoroutine(startColor, endColor, time));
    }

    public IEnumerator BlinkCoroutine(Color startColor, Color endColor, float time)
    {
        MeshRenderer blinkTarget = GetComponent<MeshRenderer>();
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
}
