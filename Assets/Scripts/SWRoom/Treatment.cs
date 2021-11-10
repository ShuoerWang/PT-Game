using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class Treatment : MonoBehaviour
{
    
    public DialogueNode greeting;
    public DialogueNode afterTreat;
    public DialogueNode[] treatments;
    public DialogueNode[] terminateTreatmentNodes;
    public DialogueNode[] faultyTreatmentReponse;
    public CommonDialogueNode[] askCounterIndicationNode;
    public DialogueNode yesNode;
    public DialogueNode noNode;

    public DialogueNode[] hints;

    public DialogueChannel dialogueChannel;

    public SWRoomManager SWM;

    void Awake()
    {
        dialogueChannel.OnDialogueEnd += onDialogueEnd;
    }

    void OnDestroy()
    {
        dialogueChannel.OnDialogueEnd -= onDialogueEnd;
    }

    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("hasCounterIndication"))
        {
            int hasCounterIndication = UnityEngine.Random.Range(0, 2);
            PlayerPrefs.SetInt("hasCounterIndication", hasCounterIndication);
            int counterIndication;
            if (hasCounterIndication == 0)
            {
                counterIndication = 0;
            }
            else
            {
                counterIndication = UnityEngine.Random.Range(1, 5);
            }
            PlayerPrefs.SetInt("counterIndication", counterIndication);

            PlayerPrefs.SetInt("hint", 0);
            PlayerPrefs.SetInt("greeting", 0);
            PlayerPrefs.SetInt("treated", 0);
            PlayerPrefs.SetInt("treatedForAWhile", 0);
            PlayerPrefs.SetInt("powerPlugged", 0);
            PlayerPrefs.SetInt("wandPlugged", 0);
            PlayerPrefs.SetInt("machineOn", 0);
            PlayerPrefs.SetInt("fkedUp", 0);

            ChangeDialogueNode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (PlayerPrefs.GetInt("hint") == 0)
        {
            PlayerPrefs.SetInt("hint", 1);
            dialogueChannel.RaiseDialogueNodeStart(hints[0]);
        }
        if (PlayerPrefs.GetInt("treatedForAWhile") == 1
            && PlayerPrefs.GetInt("hasCounterIndication") == 1
            && PlayerPrefs.GetInt("fkedUp") == 0)
        {
            PlayerPrefs.SetInt("fkedUp", 1);
            dialogueChannel.RaiseDialogueNodeStart(faultyTreatmentReponse[0]);
        }
    }

    public void ChangeDialogueNode()
    {
        for (int i = 0; i < askCounterIndicationNode.Length; i++)
        {
            if (PlayerPrefs.GetInt("counterIndication") - 1 == i)
            {
                askCounterIndicationNode[i].changeNext(yesNode);
            } else
            {
                askCounterIndicationNode[i].changeNext(noNode);
            }
        }
    }

    public void triggerDialogue()
    {
        if (PlayerPrefs.GetInt("treatedForAWhile") == 0)
        {
            dialogueChannel.RaiseDialogueNodeStart(greeting);
        } else
        {
            dialogueChannel.RaiseDialogueNodeStart(afterTreat);
        }
    }

    private void onDialogueEnd(DialogueNode dialogueNode)
    {
        if (Array.IndexOf(terminateTreatmentNodes, dialogueNode) > -1)
        {
            SceneManager.LoadScene("End");
        }
    }

    public void triggerMachinePowerHint()
    {
        dialogueChannel.RaiseDialogueNodeStart(hints[2]);
    }

    public void triggerMachineHint()
    {
        dialogueChannel.RaiseDialogueNodeStart(hints[1]);
    }

    public void triggerMachinePlugHint()
    {
        dialogueChannel.RaiseDialogueNodeStart(hints[4]);
    }

}
