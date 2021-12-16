using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Manage the navigation through Arrows
public class SWRoomManager : MonoBehaviour
{
    public GameObject entry;
    public GameObject entry2;
    public GameObject foot;
    public GameObject back;
    public GameObject machine;
    public GameObject patient;
    public GameObject elbow;

    private GameObject currMachinePowerPlug;
    public GameObject machinePowerPlug;
    public GameObject machinePowerPlugOn;

    private GameObject currMachineWandPlug;
    public GameObject machineWandPlug;
    public GameObject machineWandPlugOn;
    public GameObject shelf;

    public SpriteRenderer machineSprite;
    public Sprite machinePowerOn;
    public Sprite machinePowerOff;

    public SpriteRenderer patientSprite;
    public Sprite patientFadeOut;

    public GameObject forwardArrow;
    public GameObject backArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public DialogueChannel dialogueChannel;
    public GameObject dialogueButton;

    private GameObject current;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("patient") == 1)
        {
            Camera.main.transform.position =
                            new Vector3(entry2.transform.position.x,
                            entry2.transform.position.y, Camera.main.transform.position.z);
            current = entry2;
        } else
        {
            current = entry;
        }


        if (PlayerPrefs.GetInt("powerPlugged") == 1)
        {
            currMachinePowerPlug = machinePowerPlugOn;
        } else
        {
            currMachinePowerPlug = machinePowerPlug;
        }
        if (PlayerPrefs.GetInt("wandPlugged") == 1)
        {
            currMachineWandPlug = machineWandPlugOn;
        } else
        {
            currMachineWandPlug = machineWandPlug;
        }
        if (PlayerPrefs.GetInt("machineOn") == 1)
        {
            machineSprite.sprite = machinePowerOn;
        } else if (PlayerPrefs.GetInt("machineOn") == 0)
        {
            machineSprite.sprite = machinePowerOff;
        }

        if (PlayerPrefs.GetInt("fkedUp") > 0)
        {
            PlayerPrefs.SetInt("fkedUp", 2);
            patientSprite.sprite = patientFadeOut;
        }

        updateCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (PlayerPrefs.GetInt("fkedUp") == 1)
        {
            PlayerPrefs.SetInt("fkedUp", 2);
            patientSprite.sprite = patientFadeOut;
        }
    }

    void Awake()
    {
        dialogueChannel.OnDialogueEnd += finishDialogue;
        dialogueChannel.OnDialogueStart += startDialogue;
        dialogueButton.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        dialogueChannel.OnDialogueEnd -= finishDialogue;
        dialogueChannel.OnDialogueStart -= startDialogue;
    }

    public void MoveForward()
    {
        if (current == entry)
        {
            Camera.main.transform.position =
                            new Vector3(back.transform.position.x,
                            back.transform.position.y, Camera.main.transform.position.z);
            current = back;
            updateCanvas();
        }
        else if (current == machine)
        {
            Camera.main.transform.position =
                            new Vector3(currMachineWandPlug.transform.position.x,
                            currMachineWandPlug.transform.position.y, Camera.main.transform.position.z);
            current = currMachineWandPlug;
            updateCanvas();
        }
        else if (current == shelf)
        {
            Camera.main.transform.position =
                            new Vector3(machine.transform.position.x,
                            machine.transform.position.y, Camera.main.transform.position.z);
            current = machine;
            updateCanvas();
        }
        else if (current == elbow)
        {
            Camera.main.transform.position =
                            new Vector3(entry2.transform.position.x,
                            entry2.transform.position.y, Camera.main.transform.position.z);
            current = entry2;
            updateCanvas();
        }

    }

    public void MoveBack()
    {
        if (current == currMachineWandPlug)
        {
            Camera.main.transform.position =
                new Vector3(machine.transform.position.x,
                machine.transform.position.y, Camera.main.transform.position.z);
            current = machine;
        }
        else if (current == back
            || current == foot || current == patient)
        {
            Camera.main.transform.position =
                new Vector3(entry.transform.position.x,
                entry.transform.position.y, Camera.main.transform.position.z);
            current = entry;
        }
        else if (current == machine)
        {
            Camera.main.transform.position =
                new Vector3(shelf.transform.position.x,
                shelf.transform.position.y, Camera.main.transform.position.z);
            current = shelf;
        }
        else if (current == entry2)
        {
            Camera.main.transform.position =
                new Vector3(elbow.transform.position.x,
                elbow.transform.position.y, Camera.main.transform.position.z);
            current = elbow;
        }
        updateCanvas();
    }

    public void MoveLeft()
    {
        if (current == back)
        {
            Camera.main.transform.position =
                new Vector3(foot.transform.position.x,
                foot.transform.position.y, Camera.main.transform.position.z);
            current = foot;
        }
        else if (current == patient)
        {
            Camera.main.transform.position =
                new Vector3(back.transform.position.x,
                back.transform.position.y, Camera.main.transform.position.z);
            current = back;
        }
        else if (current == currMachinePowerPlug)
        {
            Camera.main.transform.position =
                new Vector3(shelf.transform.position.x,
                shelf.transform.position.y, Camera.main.transform.position.z);
            current = shelf;
        } else if (current == elbow || current == entry || current == entry2)
        {
            MoveToMachine();
        }
        updateCanvas();
    }

    public void MoveRight()
    {
        if (current == back)
        {
            Camera.main.transform.position =
                new Vector3(patient.transform.position.x,
                patient.transform.position.y, Camera.main.transform.position.z);
            current = patient;
        }
        else if (current == foot)
        {
            Camera.main.transform.position =
                new Vector3(back.transform.position.x,
                back.transform.position.y, Camera.main.transform.position.z);
            current = back;
        }
        else if (current == shelf)
        {
            Camera.main.transform.position =
                new Vector3(currMachinePowerPlug.transform.position.x,
                currMachinePowerPlug.transform.position.y, Camera.main.transform.position.z);
            current = currMachinePowerPlug;
        } else if (current == machine)
        {
            if (PlayerPrefs.GetInt("patient") == 1)
            {
                Camera.main.transform.position =
                new Vector3(entry2.transform.position.x,
                entry2.transform.position.y, Camera.main.transform.position.z);
                current = entry2;
            }
            else
            {
                Camera.main.transform.position =
                new Vector3(entry.transform.position.x,
                entry.transform.position.y, Camera.main.transform.position.z);
                current = entry;
            }
        }
        updateCanvas();
    }

    public void MoveToFoot()
    {
        Camera.main.transform.position =
                new Vector3(foot.transform.position.x,
                foot.transform.position.y, Camera.main.transform.position.z);
        current = foot;
        updateCanvas();
    }

    public void MoveToBack()
    {
        Camera.main.transform.position =
                            new Vector3(back.transform.position.x,
                            back.transform.position.y, Camera.main.transform.position.z);
        current = back;
        updateCanvas();
    }

    public void MoveToPatient()
    {
        Camera.main.transform.position =
                new Vector3(patient.transform.position.x,
                patient.transform.position.y, Camera.main.transform.position.z);
        current = patient;
        updateCanvas();
    }

    public void MoveToMachine()
    {
        Camera.main.transform.position =
                new Vector3(machine.transform.position.x,
                machine.transform.position.y, Camera.main.transform.position.z);
        current = machine;
        updateCanvas();
    }

    // for different sub-scene in this scene, update the navigation arrow
    // add a if else branch if add a new subscene
    // To modify the arrows show up in each scene, do it here!
    public void updateCanvas()
    {
        if (current == entry)
        {
            forwardArrow.SetActive(true);
            backArrow.SetActive(false);
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
            dialogueButton.SetActive(false);
        }
        else if (current == machine)
        {
            forwardArrow.SetActive(true);
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
            dialogueButton.SetActive(false);
        }
        else if (current == back)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            dialogueButton.SetActive(false);
        }
        else if (current == foot)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
            dialogueButton.SetActive(false);
        }
        else if (current == patient)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
            dialogueButton.SetActive(true);
        }
        else if (current == currMachinePowerPlug)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(false);
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
            dialogueButton.SetActive(false);
        }
        else if (current == currMachineWandPlug)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            dialogueButton.SetActive(false);
        }
        else if (current == shelf)
        {
            forwardArrow.SetActive(true);
            backArrow.SetActive(false);
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
            dialogueButton.SetActive(false);
        }
        else if (current == entry2)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
            dialogueButton.SetActive(true);
        }
        else if (current == elbow)
        {
            forwardArrow.SetActive(true);
            backArrow.SetActive(false);
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
            dialogueButton.SetActive(false);
        }
    }

    private void finishDialogue(DialogueNode dialogueNode)
    {
        updateCanvas();
    }


    private void startDialogue(DialogueNode dialogueNode)
    {
        forwardArrow.SetActive(false);
        backArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }

    public void disableArrows()
    {
        forwardArrow.SetActive(false);
        backArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }

    public void changeToMO()
    {
        machineSprite.sprite = machinePowerOn;
    }

    public void changeToMOFF()
    {
        machineSprite.sprite = machinePowerOff;
    }

    public void powerPlugged()
    {
        currMachinePowerPlug = machinePowerPlugOn;
        current = currMachinePowerPlug;
        Camera.main.transform.position =
                new Vector3(currMachinePowerPlug.transform.position.x,
                currMachinePowerPlug.transform.position.y, Camera.main.transform.position.z);
        updateCanvas();
    }

    public void wandPlugged()
    {
        currMachineWandPlug = machineWandPlugOn;
        current = currMachineWandPlug;
        Camera.main.transform.position =
                new Vector3(currMachineWandPlug.transform.position.x,
                currMachineWandPlug.transform.position.y, Camera.main.transform.position.z);
        updateCanvas();
    }

    public void ResetGame()
    {
        if (PlayerPrefs.GetInt("patient") == 1)
        {
            Camera.main.transform.position =
                            new Vector3(entry2.transform.position.x,
                            entry2.transform.position.y, Camera.main.transform.position.z);
            current = entry2;
        }
        else
        {
            Camera.main.transform.position =
                            new Vector3(entry.transform.position.x,
                            entry.transform.position.y, Camera.main.transform.position.z);
            current = entry;
        }
        changeToMOFF();
        currMachineWandPlug = machineWandPlug;
        currMachinePowerPlug = machinePowerPlug;


    }
}
