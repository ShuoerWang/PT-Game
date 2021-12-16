using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class InputDialogueBox : MonoBehaviour
{
    private InputDialogueNode inputDialogueNode;
    public TMP_InputField inputField;
    public DialogueChannel dialogueChannel;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void clickConfirmButton()
    {
        string input = inputField.text;
        DialogueNode nextNode = inputDialogueNode.getNext(input);
        if (nextNode)
        {
            dialogueChannel.RaiseDialogueNodeEnd();
            dialogueChannel.RaiseDialogueNodeStart(nextNode);
        } else
        {
            dialogueChannel.RaiseDialogueNodeEnd();
            dialogueChannel.RaiseDialogueEnd(inputDialogueNode);
        }
    }

    public void setInputDialogueNode(InputDialogueNode inputDialogueNode)
    {
        this.inputDialogueNode = inputDialogueNode;
    }
}
