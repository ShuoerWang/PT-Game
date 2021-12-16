using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//Manage the dialogues during the treatment
public class Treatment : MonoBehaviour
{
    
    public DialogueNode greeting;
    public DialogueNode afterTreat;
    public DialogueNode[] treatments;
    public DialogueNode[] faultyTreatmentReponse;
    public CommonDialogueNode[] askCounterIndicationNode;
    public DialogueNode yesNode;
    public DialogueNode noNode;
    public CommonDialogueNode askPainArea;
    public DialogueNode footArea;
    public DialogueNode elbowArea;

    public DialogueNode[] hints;

    public DialogueChannel dialogueChannel;

    public SWRoomManager SWM;

    public Results results;

    public DialogueNode[] TargetNodes;

    void Awake()
    {
        resetToNewGame();
    }

    void OnDestroy()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        
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
            if (PlayerPrefs.GetInt("patient") == 0)
            {
                dialogueChannel.RaiseDialogueNodeStart(hints[0]);
            } else
            {
                dialogueChannel.RaiseDialogueNodeStart(hints[6]);
            }
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
        if (PlayerPrefs.GetInt("patient") == 1)
        {
            askPainArea.changeNext(elbowArea);
        }
        else
        {
            askPainArea.changeNext(footArea);
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

    public void triggerMachineOffWandhint()
    {
        dialogueChannel.RaiseDialogueNodeStart(hints[5]);
    }

    public void triggerMachineOnWandhint()
    {
        dialogueChannel.RaiseDialogueNodeStart(hints[3]);
    }

    public void setUpGameState()
    {
        if (!PlayerPrefs.HasKey("hasCounterIndication"))
        {
            int hasCounterIndication = UnityEngine.Random.Range(0, 10);
            if (hasCounterIndication > 7)
            {
                PlayerPrefs.SetInt("hasCounterIndication", 1);
            }
            else
            {
                PlayerPrefs.SetInt("hasCounterIndication", 0);
            }
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
            PlayerPrefs.SetInt("patient", UnityEngine.Random.Range(0, 2));

            PlayerPrefs.SetInt("hint", 0);
            PlayerPrefs.SetInt("greeting", 0);
            PlayerPrefs.SetInt("treated", 0);
            PlayerPrefs.SetInt("treatedForAWhile", 0);
            PlayerPrefs.SetInt("powerPlugged", 0);
            PlayerPrefs.SetInt("wandPlugged", 0);
            PlayerPrefs.SetInt("machineOn", 0);
            PlayerPrefs.SetInt("fkedUp", 0);
            PlayerPrefs.SetInt("glove", 0);
            PlayerPrefs.SetInt("marker", 0);
            PlayerPrefs.SetInt("gel", 0);
            PlayerPrefs.SetInt("wand", 0);
            PlayerPrefs.SetString("currentTool", "");
            PlayerPrefs.SetInt("terminated", 0);

            ChangeDialogueNode();
        }
        
    }

    public void resetToNewGame()
    {
        int hasCounterIndication = UnityEngine.Random.Range(0, 10);
        if (hasCounterIndication > 7)
        {
            PlayerPrefs.SetInt("hasCounterIndication", 1);
        }
        else
        {
            PlayerPrefs.SetInt("hasCounterIndication", 0);
        }
        int counterIndication;
        if (hasCounterIndication <= 7)
        {
            counterIndication = 0;
        }
        else
        {
            counterIndication = UnityEngine.Random.Range(1, 5);
        }
        PlayerPrefs.SetInt("counterIndication", counterIndication);
        PlayerPrefs.SetInt("patient", UnityEngine.Random.Range(0, 2));
        if (PlayerPrefs.GetInt("patient") == 0)
        {
            PlayerPrefs.SetString("patientName", "Lucy");
        }
        else
        {
            PlayerPrefs.SetString("patientName", "Ilan");
        }


        PlayerPrefs.SetInt("hint", 0);
        PlayerPrefs.SetInt("greeting", 0);
        PlayerPrefs.SetInt("treated", 0);
        PlayerPrefs.SetInt("treatedForAWhile", 0);
        PlayerPrefs.SetInt("powerPlugged", 0);
        PlayerPrefs.SetInt("wandPlugged", 0);
        PlayerPrefs.SetInt("machineOn", 0);
        PlayerPrefs.SetInt("fkedUp", 0);
        PlayerPrefs.SetInt("glove", 0);
        PlayerPrefs.SetInt("marker", 0);
        PlayerPrefs.SetInt("gel", 0);
        PlayerPrefs.SetInt("wand", 0);
        PlayerPrefs.SetString("currentTool", "");
        PlayerPrefs.SetInt("terminated", 0);
        PlayerPrefs.SetInt("target", UnityEngine.Random.Range(0, 3));

        ChangeDialogueNode();
    }

    public void resetToPreviousGame()
    {
        PlayerPrefs.SetInt("hint", 0);
        PlayerPrefs.SetInt("greeting", 0);
        PlayerPrefs.SetInt("treated", 0);
        PlayerPrefs.SetInt("treatedForAWhile", 0);
        PlayerPrefs.SetInt("powerPlugged", 0);
        PlayerPrefs.SetInt("wandPlugged", 0);
        PlayerPrefs.SetInt("machineOn", 0);
        PlayerPrefs.SetInt("fkedUp", 0);
        PlayerPrefs.SetInt("glove", 0);
        PlayerPrefs.SetInt("marker", 0);
        PlayerPrefs.SetInt("gel", 0);
        PlayerPrefs.SetInt("wand", 0);
        PlayerPrefs.SetString("currentTool", "");
        PlayerPrefs.SetInt("terminated", 0);
    }

    public void triggerTargetCorrect()
    {
        dialogueChannel.RaiseDialogueNodeStart(TargetNodes[0]);
    }

    public void triggerTargetAlmostCorrect()
    {
        dialogueChannel.RaiseDialogueNodeStart(TargetNodes[1]);
    }

    public void triggerTargetLeft()
    {
        dialogueChannel.RaiseDialogueNodeStart(TargetNodes[2]);
    }

    public void triggerTargetRight()
    {
        dialogueChannel.RaiseDialogueNodeStart(TargetNodes[3]);
    }

    public void triggerTargetUp()
    {
        dialogueChannel.RaiseDialogueNodeStart(TargetNodes[4]);
    }

    public void triggerTargetDown()
    {
        dialogueChannel.RaiseDialogueNodeStart(TargetNodes[5]);
    }
}
