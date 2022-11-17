using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, Interactable
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private playerMoves playerMoves;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out playerMoves playerMoves))
        {
            playerMoves.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out playerMoves playerMoves))
        {
            if (playerMoves.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                playerMoves.Interactable = null;
            }
        }
    }

    public void Interact(playerMoves playerMoves)
    {        
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                playerMoves.DialogueSCRIPT.AddResponseEvents(responseEvents.Events);
                break;
            }
        } 

        /* if(TryGetComponent(out DialogueResponseEvents responseEvents) && responseEvents.DialogueObject == dialogueObject)
        {
            playerMoves.DialogueSCRIPT.AddResponseEvents(responseEvents.Events);
        } */

        playerMoves.DialogueSCRIPT.ShowDialogue(dialogueObject);
    }
}
