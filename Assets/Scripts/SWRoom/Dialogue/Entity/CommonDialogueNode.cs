using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/DialogueNode/Common")]
public class CommonDialogueNode : DialogueNode
{
    public DialogueNode nextNode;


    public override bool CanBeFollowedByNode(DialogueNode node)
    {
        return nextNode == node;
    }

    public override void Accept(DialogueNodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}
