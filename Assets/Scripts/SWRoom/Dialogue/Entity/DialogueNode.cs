using UnityEngine;

public abstract class DialogueNode : ScriptableObject
{

    public string narration;
    public string speakerName;
    
    public abstract void Accept(DialogueNodeVisitor visitor);
    public void ChangeNarration(string narration)
    {
        this.narration = narration;
    } 
}