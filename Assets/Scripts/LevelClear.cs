using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelClear : MonoBehaviour
{
    public String nextLevelSceneName;
    public GameObject transition;
    public GameObject CanvasUI;
    public float timeDelay;
    public TMP_Text clearText;

    private bool isNivelClear = false;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        clearText.gameObject.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isNivelClear)
        {
            makeLevelClear(nextLevelSceneName);
        }
    }

    void makeLevelClear(String nextLevelSceneName)
    {
        Instantiate(transition, CanvasUI.transform);
        StartCoroutine(sceneLoadingDelay(nextLevelSceneName));
        isNivelClear = true;
    }

    IEnumerator sceneLoadingDelay(String SceneName)
    {
        yield return new WaitForSeconds(1f);
        clearText.gameObject.SetActive(true);
        clearText.text = $"Nivel completado!";
        yield return new WaitForSeconds(1f);
        clearText.text = $"Nivel completado!\nTotal de frutinhas de açai coletadas: {player.coins}";
        yield return new WaitForSeconds(1f);
        clearText.text = $"Nivel completado!\nTotal de frutinhas de açai coletadas: {player.coins}\nBom trabalho!";
        yield return new WaitForSeconds(1f);
        clearText.text = $"Nivel completado!\nTotal de frutinhas de açai coletadas: {player.coins}\nBom trabalho!\nCarregando proximo nivel...";
        yield return new WaitForSeconds(timeDelay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
}
