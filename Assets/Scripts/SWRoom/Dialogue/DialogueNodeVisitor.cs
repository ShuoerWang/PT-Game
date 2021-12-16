public interface DialogueNodeVisitor
{
    void Visit(CommonDialogueNode node);
    void Visit(ChoiceDialogueNode node);
    void Visit(InputDialogueNode node);
}