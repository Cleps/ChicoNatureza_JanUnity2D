using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject choosePanel;
    public TMP_Text dialogueText;
    public List<string> dialogues;

    [System.Serializable]
    public class DialogueOption
    {
        public string optionText;
        public List<string> optionDialogues;
    }

    public bool hasOptions = false;
    public List<DialogueOption> options;
    public List<TMP_Text> optionTexts;

    private int currentDialogueIndex = 0;
    private bool isPlayerInRange = false;
    private bool isChoosingOption = false;
    private int currentOptionIndex = 0;

    private bool isOptionDialogue = false;
    private int currentOptionDialogueIndex = 0;

    private AudioSource audioSource;
    public AudioClip dialogueSound;

    // NOVO: salvar última opção escolhida
    private bool hasChosenOption = false;
    private int lastOptionIndex = -1;
    private int lastOptionDialogueIndex = 0;

    void Start()
    {
        dialoguePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        foreach (var txt in optionTexts) txt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeSelf)
            GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().canMove = false;

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueSound != null)
                audioSource.PlayOneShot(dialogueSound);

            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                if (hasChosenOption)
                    ContinueLastOptionDialogue(); // Se já escolheu uma opção antes, continua ela
                else
                    ShowDialogue(); // Senão, começa diálogo normal
            }
            else
            {
                if (isOptionDialogue)
                {
                    NextOptionDialogue();
                }
                else if (!isChoosingOption)
                {
                    NextDialogue();
                }
                else
                {
                    ConfirmOption();
                }
            }
        }

        if (isChoosingOption && options.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                currentOptionIndex = (currentOptionIndex - 1 + options.Count) % options.Count;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                currentOptionIndex = (currentOptionIndex + 1) % options.Count;
            }

            UpdateOptionDisplay();
        }
    }

    void ShowDialogue()
    {
        if (currentDialogueIndex < dialogues.Count)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
        }
        else
        {
            if (hasOptions && options.Count > 0)
                ShowOptions();
            else
                EndDialogue();
        }
    }

    void NextDialogue()
    {
        currentDialogueIndex++;
        ShowDialogue();
    }

    void ShowOptions()
    {
        isChoosingOption = true;

        for (int i = 0; i < options.Count; i++)
        {
            optionTexts[i].gameObject.SetActive(true);
            optionTexts[i].text = options[i].optionText;
        }

        choosePanel.SetActive(true);
        UpdateOptionDisplay();
    }

    void UpdateOptionDisplay()
    {
        for (int i = 0; i < options.Count; i++)
        {
            optionTexts[i].color = (i == currentOptionIndex) ? Color.blue : Color.black;
        }
    }

    void ConfirmOption()
    {
        isChoosingOption = false;
        isOptionDialogue = true;
        currentOptionDialogueIndex = 0;

        // Salvar a opção escolhida
        hasChosenOption = true;
        lastOptionIndex = currentOptionIndex;
        lastOptionDialogueIndex = 0;

        foreach (var txt in optionTexts)
            txt.gameObject.SetActive(false);

        choosePanel.SetActive(false);

        dialogueText.text = options[currentOptionIndex].optionDialogues[currentOptionDialogueIndex];
    }

    void NextOptionDialogue()
    {
        currentOptionDialogueIndex++;

        if (currentOptionDialogueIndex < options[currentOptionIndex].optionDialogues.Count)
        {
            dialogueText.text = options[currentOptionIndex].optionDialogues[currentOptionDialogueIndex];
        }
        else
        {
            isOptionDialogue = false;

            // Salvar o ponto onde o diálogo terminou
            lastOptionDialogueIndex = options[currentOptionIndex].optionDialogues.Count - 1;

            EndDialogue();
        }
    }

    // NOVO: continuar o último diálogo da opção escolhida
    void ContinueLastOptionDialogue()
    {
        isOptionDialogue = true;
        currentOptionIndex = lastOptionIndex;
        currentOptionDialogueIndex = 0;

        dialogueText.text = options[currentOptionIndex].optionDialogues[currentOptionDialogueIndex];
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().canMove = true;
        ResetDialogue();
    }

    void ResetDialogue()
    {
        currentDialogueIndex = 0;
        isChoosingOption = false;
        isOptionDialogue = false;
        currentOptionDialogueIndex = 0;

        foreach (var txt in optionTexts)
        {
            txt.gameObject.SetActive(false);
        }
        choosePanel.SetActive(false);
        // NÃO resetar hasChosenOption, lastOptionIndex, lastOptionDialogueIndex
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
            dialoguePanel.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Move>().canMove = true;
            ResetDialogue();
        }
    }
}
