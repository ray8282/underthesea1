using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SansDialogue : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    [Header("Dialogue")]
    [TextArea(2, 5)]
    public string[] dialogueLines =
    {
        "와! 샌즈!",
        "언더테일 아시는구나",
        "진.짜.겁.나.어.렵.습.니.다.",
    };

    private bool playerNearby = false;
    private bool isTalking = false;
    private int currentLine = 0;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTalking)
            {
                StartDialogue();
            }
            else
            {
                NextDialogue();
            }
        }
    }

    void StartDialogue()
    {
        isTalking = true;
        currentLine = 0;

        dialoguePanel.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];

        SetPlayerMovement(false);
    }

    void NextDialogue()
    {
        currentLine++;

        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogueLines[currentLine];
    }

    void EndDialogue()
    {
        isTalking = false;

        dialoguePanel.SetActive(false);

        SetPlayerMovement(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    void SetPlayerMovement(bool canMove)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        MovingObject movement = player.GetComponent<MovingObject>();

        if (movement != null)
        {
            movement.enabled = canMove;
        }
    }
}