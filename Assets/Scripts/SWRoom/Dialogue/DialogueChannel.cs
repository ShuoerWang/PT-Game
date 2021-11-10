using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue/Dialogue Channel")]
public class DialogueChannel : ScriptableObject
{
    public delegate void DialogueCallback(DialogueNode node);
    public delegate void DialogueEndCallback();

    public DialogueCallback OnDialogueEnd;
    public DialogueEndCallback OnDialogueNodeEnd;

    public DialogueCallback OnDialogueStart;


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
}