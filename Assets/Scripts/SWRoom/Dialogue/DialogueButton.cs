using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    // Start is called before the first frame update
    public DialogueChannel dialogueChannel;
    public Treatment treatment;

    void Start()
    {
        dialogueChannel.OnDialogueStart += hideButton;

    }

    void OnDestroy()
    {
        dialogueChannel.OnDialogueStart -= hideButton;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void hideButton(DialogueNode dialogueNode)
    {
        gameObject.SetActive(false);
    }

    public void trggerDialogue()
    {
        treatment.triggerDialogue();
    }
}
