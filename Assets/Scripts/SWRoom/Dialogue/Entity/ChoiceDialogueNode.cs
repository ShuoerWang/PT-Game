using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class DialogueChoice
{
    public string preview;
    public DialogueNode choiceNode;
}

[CreateAssetMenu(menuName = "Scriptable Objects/DialogueNode/Choice")]
public class ChoiceDialogueNode : DialogueNode
{
    public DialogueChoice[] Choices;


    public override bool CanBeFollowedByNode(DialogueNode node)
    {
        return Choices.Any(x => x.choiceNode == node);
    }

    public override void Accept(DialogueNodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}