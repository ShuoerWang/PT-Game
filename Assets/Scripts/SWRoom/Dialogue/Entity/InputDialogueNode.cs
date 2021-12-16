using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class InputMap
{
    public string counterIndication;
    public DialogueNode counterIndicationNode;
}

[CreateAssetMenu(menuName = "Scriptable Objects/DialogueNode/Input")]
public class InputDialogueNode : DialogueNode
{
    public InputMap[] inputMap;

    public override void Accept(DialogueNodeVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void changeNext(string input, CommonDialogueNode node)
    {
        for (int i = 0; i < inputMap.Length; i++)
        {
            if (inputMap[i].counterIndication.Equals(input))
            {
                inputMap[i].counterIndicationNode = node;
            }
        }
    }

    public DialogueNode getNext(string input)
    {
        for (int i = 0; i < inputMap.Length; i++)
        {
            if (inputMap[i].counterIndication.Contains(input))
            {
                return inputMap[i].counterIndicationNode;
            }
        }

        return inputMap[0].counterIndicationNode;
    }
}
