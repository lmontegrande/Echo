using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public Image deathImage;
    public Text sonarInstructionText;
    public Text movementInstructionText;
    public float delayDeathTime = 1f;
    public float instructionTime = 5f;

    private Coroutine currentDieCoroutine;
    private float instructionTimer = 0f;
    private bool doneShowingInstructions = false;

    public void Update()
    {
        if (!doneShowingInstructions && sonarInstructionText.enabled == false)
            UpdateTimer();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "EchoCube")
            other.GetComponent<EchoCube>().Blink(Color.yellow, Color.black, 1f);
    }

    public void Die()
    {
        if (currentDieCoroutine != null)
            return;

        GetComponent<PlayerController>().isDead = true;
        currentDieCoroutine = StartCoroutine(DieCoroutine());
    }

    public void FinishTutorial()
    {
        sonarInstructionText.enabled = false;
    }

    private void UpdateTimer()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            instructionTimer += Time.deltaTime;
        } else {
            instructionTimer = 0;
            doneShowingInstructions = true;
        }

        movementInstructionText.enabled = instructionTimer >= instructionTime;
    }

    private IEnumerator DieCoroutine()
    {
        deathImage.enabled = true;
        AudioManager.instance.PlayDeathAudioClip();
        yield return new WaitForSeconds(delayDeathTime);
        GameObject.Find("GameManager").GetComponent<GameManager>().LoadIntro();
    }
}
