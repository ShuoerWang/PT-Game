using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/DialogueNode/Common")]
public class CommonDialogueNode : DialogueNode
{
    public DialogueNode nextNode;

    public override void Accept(DialogueNodeVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void changeNext(DialogueNode node)
    {
        nextNode = node;
    }
}
