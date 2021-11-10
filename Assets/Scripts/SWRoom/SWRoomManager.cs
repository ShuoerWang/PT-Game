using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SWRoomManager : MonoBehaviour
{
    public GameObject entry;
    public GameObject foot;
    public GameObject back;
    public GameObject machine;
    public GameObject patient;

    public GameObject machinePowerPlug;
    public GameObject machinePowerPlugOn;
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
        if (PlayerPrefs.GetString("currentScene").Equals("back"))
        {
            current = back;
        } else
        {
            current = entry;
        }

        if (PlayerPrefs.GetInt("powerPlugged") == 1)
        {
            machinePowerPlug = machinePowerPlugOn;
        }
        if (PlayerPrefs.GetInt("wandPlugged") == 1)
        {
            machineWandPlug = machineWandPlugOn;
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
                            new Vector3(machineWandPlug.transform.position.x,
                            machineWandPlug.transform.position.y, Camera.main.transform.position.z);
            current = machineWandPlug;
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
    }

    public void MoveBack()
    {
        if (current == entry || current == machineWandPlug)
        {
            Camera.main.transform.position =
                new Vector3(machine.transform.position.x,
                machine.transform.position.y, Camera.main.transform.position.z);
            current = machine;
        }
        else if (current == back
            || current == foot || current == patient || current == shelf)
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
        else if (current == machinePowerPlug)
        {
            Camera.main.transform.position =
                new Vector3(shelf.transform.position.x,
                shelf.transform.position.y, Camera.main.transform.position.z);
            current = shelf;
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
                new Vector3(machinePowerPlug.transform.position.x,
                machinePowerPlug.transform.position.y, Camera.main.transform.position.z);
            current = machinePowerPlug;
        }
        updateCanvas();
    }

    // for different sub-scene in this scene, update the navigation arrow
    // add a if else branch if add a new subscene
    public void updateCanvas()
    {
        if (current == entry)
        {
            forwardArrow.SetActive(true);
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            dialogueButton.SetActive(false);
        }
        else if (current == machine)
        {
            forwardArrow.SetActive(true);
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
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
        else if (current == machinePowerPlug)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(false);
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
            dialogueButton.SetActive(false);
        }
        else if (current == machineWandPlug)
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
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
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
        machinePowerPlug = machinePowerPlugOn;
        current = machinePowerPlugOn;
        Camera.main.transform.position =
                new Vector3(machinePowerPlug.transform.position.x,
                machinePowerPlug.transform.position.y, Camera.main.transform.position.z);
        updateCanvas();
    }

    public void wandPlugged()
    {
        machineWandPlug = machineWandPlugOn;
        current = machineWandPlugOn;
        Camera.main.transform.position =
                new Vector3(machineWandPlug.transform.position.x,
                machineWandPlug.transform.position.y, Camera.main.transform.position.z);
        updateCanvas();
    }
}
