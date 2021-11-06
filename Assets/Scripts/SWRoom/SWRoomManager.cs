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

    public GameObject forwardArrow;
    public GameObject backArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public DialogueChannel dialogueChannel;

    private GameObject current;
    public DialogueNode greetingDialogue;
    private bool greeted;
    private bool showDialogue;

    // Start is called before the first frame update
    void Start()
    {
        current = entry;
        updateArrows();
    }

    // Update is called once per frame
    void Update()
    {
        if (current == patient && !greeted && !showDialogue)
        {
            dialogueChannel.RaiseDialogueNodeStart(greetingDialogue);
            showDialogue = true;
        }
    }

    void Awake()
    {
        dialogueChannel.OnDialogueEnd += finishDialogue;
        dialogueChannel.OnDialogueStart += startDialogue;
        greeted = false;
        showDialogue = false;
    }

    public void MoveForward()
    {
        Camera.main.transform.position =
                new Vector3(back.transform.position.x,
                back.transform.position.y, Camera.main.transform.position.z);
        current = back;
        updateArrows();
    }

    public void MoveBack()
    {
        if (current == entry)
        {
            Camera.main.transform.position =
                new Vector3(machine.transform.position.x,
                machine.transform.position.y, Camera.main.transform.position.z);
            current = machine;
        }
        else if (current == machine || current == back
            || current == foot || current == patient)
        {
            Camera.main.transform.position =
                new Vector3(entry.transform.position.x,
                entry.transform.position.y, Camera.main.transform.position.z);
            current = entry;
        }
        updateArrows();
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
        updateArrows();
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
        updateArrows();
    }

    // for different sub-scene in this scene, update the navigation arrow
    // add a if else branch if add a new subscene
    public void updateArrows()
    {
        if (current == entry)
        {
            forwardArrow.SetActive(true);
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }
        else if (current == machine)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }
        else if (current == back)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }
        else if (current == foot)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
        else if (current == patient)
        {
            forwardArrow.SetActive(false);
            backArrow.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
        }
    }

    private void finishDialogue()
    {
        updateArrows();
    }

    

    private void checkAndShowDialogue()
    {
        if (current == patient)
        {
            dialogueChannel.RaiseDialogueNodeStart(greetingDialogue);
            showDialogue = true;
        }
    }

    private void startDialogue(DialogueNode dialogueNode)
    {
        forwardArrow.SetActive(false);
        backArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);
    }
}
