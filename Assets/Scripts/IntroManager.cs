using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour {
    
    public Button loadSceneButton;
    public string levelToLoadString;
    public float loadDelayTime;

    private AudioSource _audioSource;

    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void LoadScene()
    {
        loadSceneButton.interactable = false;
        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        float loadDelayTimer = 0f;
        AudioManager.instance.PlayIntroAudioClip();
        while (loadDelayTimer < loadDelayTime)
        {
            _audioSource.volume = Mathf.Lerp(1, 0, Mathf.Pow(loadDelayTimer/loadDelayTime, 2));

            loadDelayTimer += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(levelToLoadString);
    }
}
