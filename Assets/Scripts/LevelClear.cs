using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelClear : MonoBehaviour
{
    public bool isFinalLevel = false;
    public String nextLevelSceneName;
    public GameObject transition;
    public GameObject CanvasUI;
    public float timeDelay;
    public TMP_Text clearText;

    private bool isNivelClear = false;
    private Player player;
    private AudioSource audioSource;
    public AudioClip levelClearSound;

    // Start is called before the first frame update
    void Start()
    {
        clearText.gameObject.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
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
        if (levelClearSound != null)
        {
            audioSource.PlayOneShot(levelClearSound);
        }
        Instantiate(transition, CanvasUI.transform);
        StartCoroutine(sceneLoadingDelay(nextLevelSceneName));
        isNivelClear = true;
    }

    IEnumerator sceneLoadingDelay(String SceneName)
    {
        if (!isFinalLevel)
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
        }
        else
        {
            yield return new WaitForSeconds(1f);
            clearText.gameObject.SetActive(true);
            clearText.text = $"Nivel completado!";
            yield return new WaitForSeconds(1f);
            clearText.text = $"Nivel completado!\nTotal de frutinhas de açai coletadas: {player.coins}";
            yield return new WaitForSeconds(1f);
            clearText.text = $"Nivel completado!\nTotal de frutinhas de açai coletadas: {player.coins}\nÓtimo trabalho!";
            yield return new WaitForSeconds(1f);
            clearText.text = $"Nivel completado!\nTotal de frutinhas de açai coletadas: {player.coins}\nBom trabalho!\nEste foi o nosso ultimo nivel!";
            yield return new WaitForSeconds(1f);
            clearText.text = $"Nivel completado!\nTotal de frutinhas de açai coletadas: {player.coins}\nBom trabalho!\nEste foi o nosso ultimo nivel!\nMuito Obrigado por finalizar nosso game!";
            yield return new WaitForSeconds(3f);
            clearText.text = $"Nivel completado!\nTotal de frutinhas de açai coletadas: {player.coins}\nBom trabalho!\nEste foi o nosso ultimo nivel!\nMuito Obrigado por finalizar nosso game!\nRedirecionando para o menu inicial...";

            // Adicionar os nomes dos integrantes da equipe com delay de 1 segundo
            yield return new WaitForSeconds(3f);
            clearText.text += "\n- Equipe Flora (Kgame) -";
            yield return new WaitForSeconds(3f);
            clearText.text += "\nProdutor: Francisco Moreira";
            yield return new WaitForSeconds(3f);
            clearText.text += "\nProgramador: Clécio Elias";
            yield return new WaitForSeconds(3f);
            clearText.text += "\nDesign de Som: Júlio Veras";
            yield return new WaitForSeconds(3f);
            clearText.text += "\nDiretor de Arte: Otto Abreu";

        }
        yield return new WaitForSeconds(timeDelay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
}
