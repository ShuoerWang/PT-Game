using UnityEngine;

public abstract class DialogueNode : ScriptableObject
{

    public string narration;
    public string speakerName;

    public abstract bool CanBeFollowedByNode(DialogueNode node);
    public abstract void Accept(DialogueNodeVisitor visitor);
}