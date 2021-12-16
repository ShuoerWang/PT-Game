using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ChoiceBox : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public DialogueChannel dialogueChannel;

    private DialogueNode nextNode;
    public DialogueNode currNode;

    public DialogueChoice Choice
    {
        set
        {
            textMeshProUGUI.text = value.preview;
            nextNode = value.choiceNode;
        }

    }

    private void Start()
    {
        //GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (nextNode)
        {
            dialogueChannel.RaiseDialogueNodeEnd();
            dialogueChannel.RaiseDialogueNodeStart(nextNode);
            dialogueChannel.RaiseDialogueChoiceMade(textMeshProUGUI.text);
        } else
        {
            dialogueChannel.RaiseDialogueNodeEnd();
            dialogueChannel.RaiseDialogueEnd(currNode);
            dialogueChannel.RaiseDialogueChoiceMade(textMeshProUGUI.text);
        }
        
    }
}
