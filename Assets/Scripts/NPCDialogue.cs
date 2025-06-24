using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel; // Painel de diálogo
    public TMP_Text dialogueText; // Texto do TMP para exibir os diálogos
    public List<string> dialogues; // Lista de diálogos
    public List<string> secondaryDialogues; // Lista de diálogos
    private int currentDialogueIndex = 0; // Índice do diálogo atual
    private bool isPlayerInRange = false; // Verifica se o jogador está na área de colisão
    private bool hasDialogueStarted = false; // Verifica se o diálogo já foi iniciado uma vez

    private AudioSource audioSource;
    public AudioClip dialogueSound;

    void Start()
    {
        dialoguePanel.SetActive(false); // Desativa o painel de diálogo no início
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(dialoguePanel.activeSelf)
        {  
            GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().canMove = false;
        }
        	

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if(dialogueSound != null)
                audioSource.PlayOneShot(dialogueSound);
            if (!dialoguePanel.activeInHierarchy)
            {
                // Abre o painel de diálogo
                dialoguePanel.SetActive(true);
                ShowDialogue();
            }
            else
            {
                // Avança para o próximo diálogo ou fecha o painel se não houver mais diálogos
                NextDialogue();
            }
        }
    }

    void ShowDialogue()
    {
        if (!hasDialogueStarted)
        {
            if (currentDialogueIndex < dialogues.Count)
            {
                dialogueText.text = dialogues[currentDialogueIndex];
            }
        }
        else
        {
            if (currentDialogueIndex < secondaryDialogues.Count)
            {
                dialogueText.text = secondaryDialogues[currentDialogueIndex];
            }
        }
    }

    void NextDialogue()
    {
        currentDialogueIndex++;
        if (!hasDialogueStarted)
        {
            if (currentDialogueIndex < dialogues.Count)
            {
                ShowDialogue();
            }
            else
            {
                // Fecha o painel de diálogo e reseta o índice
                dialoguePanel.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().canMove = true;
                currentDialogueIndex = 0;
                hasDialogueStarted = true;
            }
        }
        else
        {
            if (currentDialogueIndex < secondaryDialogues.Count)
            {
                ShowDialogue();
            }
            else
            {
                // Fecha o painel de diálogo e reseta o índice
                dialoguePanel.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().canMove = true;
                currentDialogueIndex = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Move>().canJump = false;
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            other.GetComponent<Move>().canJump = true;
            if(dialoguePanel != null)
                dialoguePanel.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().canMove = true;
            currentDialogueIndex = 0;
        }
    }
}