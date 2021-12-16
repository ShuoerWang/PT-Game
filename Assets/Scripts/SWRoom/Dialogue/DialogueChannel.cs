using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue/Dialogue Channel")]
public class DialogueChannel : ScriptableObject
{
    public delegate void DialogueCallback(DialogueNode node);
    public delegate void DialogueEndCallback();
    public delegate void DialogueChoiceMade(string choice);

    public DialogueCallback OnDialogueEnd;
    public DialogueEndCallback OnDialogueNodeEnd;

    public DialogueCallback OnDialogueStart;
    public DialogueChoiceMade onChoiceMade;


    public void RaiseDialogueNodeStart(DialogueNode node)
    {
        OnDialogueStart?.Invoke(node);
    }

    public void RaiseDialogueEnd(DialogueNode node)
    {
        OnDialogueEnd?.Invoke(node);
    }

    public void RaiseDialogueNodeEnd()
    {
        OnDialogueNodeEnd?.Invoke();
    }

    public void RaiseDialogueChoiceMade(string choice)
    {
        onChoiceMade?.Invoke(choice);
    }
}