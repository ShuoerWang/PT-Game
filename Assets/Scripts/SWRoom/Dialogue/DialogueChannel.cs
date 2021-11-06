using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialogue/Dialogue Channel")]
public class DialogueChannel : ScriptableObject
{
    public delegate void DialogueEndCallback();
    public DialogueEndCallback OnDialogueEnd;
    public DialogueEndCallback OnDialogueNodeEnd;

    public delegate void DialogueStartCallback(DialogueNode node);
    public DialogueStartCallback OnDialogueStart;


    public void RaiseDialogueNodeStart(DialogueNode node)
    {
        OnDialogueStart?.Invoke(node);
    }

    public void RaiseDialogueEnd()
    {
        OnDialogueEnd?.Invoke();
    }

    public void RaiseDialogueNodeEnd()
    {
        OnDialogueNodeEnd?.Invoke();
    }
}