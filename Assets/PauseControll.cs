using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControll : MonoBehaviour
{
    public GameObject pauseMenu; // GameObject que representa o menu de pause
    private bool isPaused = false; // Variável para controlar o estado de pausa

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false); // Certifique-se de que o menu de pause está desativado no início
    }

    // Update is called once per frame
    void Update()
    {
        // Detectar a tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true); // Ativar o menu de pause
        Time.timeScale = 0f; // Pausar o jogo
        isPaused = true;
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false); // Desativar o menu de pause
        Time.timeScale = 1f; // Retomar o jogo
        isPaused = false;
    }
}