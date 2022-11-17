using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class DialogueSCRIPT : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    //[SerializeField] private DialogueObject testDialogue;

    public bool IsOpen { get; private set; }

    private ResponsesHandler responsesHandler;
    private typeWriter typeWriter;

    private void Start()
    {
        typeWriter = GetComponent<typeWriter>();
        responsesHandler = GetComponent<ResponsesHandler>();

        CloseDialogueBox();
        //ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(routine: StepThroughDialogue(dialogueObject));
    }


    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responsesHandler.AddResponseEvents(responseEvents);
    }


    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    { 

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            //yield return typeWriter.Run(dialogue, textLabel);

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responsesHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }

    }


    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriter.Run(dialogue, textLabel);

        while (typeWriter.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typeWriter.Stop();
            }
        }
    }


    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }


    /* code cemetery | 
    {[SerializeField] private TMP_Text textLabel;
    private void Start(){
    GetComponent<typeWriter>().Run(textToType = "Première ligne \nSeconde ligne", textLabel);}} */

}