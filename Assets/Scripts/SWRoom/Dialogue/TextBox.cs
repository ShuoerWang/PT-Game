using UnityEngine;
using TMPro;

public class TextBox : MonoBehaviour, DialogueNodeVisitor
{
    private bool waitingForClick;
    private DialogueNode nextNode;
    private DialogueNode currentNode;

    public DialogueChannel dialogueChannel;
    public TextMeshProUGUI dialoguetext;
    public TextMeshProUGUI chartext;

    public ChoiceBox choiceBox;
    public RectTransform narrationBox;
    public RectTransform choicePanel;

    public void Visit(CommonDialogueNode node)
    {
        narrationBox.gameObject.SetActive(true);
        currentNode = node;
        if (node.narration.Equals(""))
        {
            narrationBox.gameObject.SetActive(false);
        }
        else {
            narrationBox.gameObject.SetActive(true);
        }
        waitingForClick = true;
        nextNode = node.nextNode;
    }

    public void Visit(ChoiceDialogueNode node)
    {
        choicePanel.gameObject.SetActive(true);
        currentNode = node;
        if (node.narration.Equals(""))
        {
            narrationBox.gameObject.SetActive(false);
        }
        else
        {
            narrationBox.gameObject.SetActive(true);
        }
        foreach (DialogueChoice choice in node.Choices)
        {
            ChoiceBox newChoice = Instantiate(choiceBox, choicePanel);
            newChoice.Choice = choice;
            newChoice.currNode = node;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        dialogueChannel.OnDialogueNodeEnd -= OnDialogueNodeEnd;
        dialogueChannel.OnDialogueStart -= OnDialogueNodeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingForClick && Input.GetMouseButtonDown(0))
        {
            if (nextNode)
            {
                dialogueChannel.RaiseDialogueNodeEnd();
                dialogueChannel.RaiseDialogueNodeStart(nextNode);
            } else
            {
                dialogueChannel.RaiseDialogueNodeEnd();
                dialogueChannel.RaiseDialogueEnd(currentNode);
            }
            
        }
    }

    void Awake()
    {
        dialogueChannel.OnDialogueStart += OnDialogueNodeStart;
        dialogueChannel.OnDialogueNodeEnd += OnDialogueNodeEnd;
        choicePanel.gameObject.SetActive(false);
        narrationBox.gameObject.SetActive(false);
    }

    private void OnDialogueNodeStart(DialogueNode dialogueNode)
    {
        gameObject.SetActive(true);
        dialoguetext.text = dialogueNode.narration;
        if (!dialogueNode.speakerName.Equals(""))
        {
            chartext.text = dialogueNode.speakerName + " :";
        }
        //TODO: speaker?

        dialogueNode.Accept(this);
    }

    private void OnDialogueNodeEnd()
    {
        waitingForClick = false;
        dialoguetext.text = "";
        chartext.text = "";
        gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(false);
        narrationBox.gameObject.SetActive(false);
        foreach (Transform child in choicePanel)
        {
            Destroy(child.gameObject);
        }
    }

}
