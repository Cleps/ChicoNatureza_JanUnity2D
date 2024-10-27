using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject transition;
    public GameObject CanvasUI;

    private AudioSource audioSource;
    public AudioClip buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene1()
    {
        Instantiate(transition, CanvasUI.transform);
        if (buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
        StartCoroutine(sceneLoadingDelay("Scene1"));
    }

    IEnumerator sceneLoadingDelay(String SceneName)
    {
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
