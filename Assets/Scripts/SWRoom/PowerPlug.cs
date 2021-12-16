using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlug : MonoBehaviour
{
    public SWRoomManager SW;
    public DialogueChannel dialogueChannel;
    public CommonDialogueNode endNode;
    public ChoiceDialogueNode startNode;

    // Start is called before the first frame update
    void Start()
    {
        dialogueChannel.OnDialogueEnd += OnDialogueEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D raycastHit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (raycastHit.transform == gameObject.transform)
            {
                if (PlayerPrefs.GetInt("powerPlugged") == 0)
                {
                    dialogueChannel.RaiseDialogueNodeStart(startNode);
                }
            }
        }
    }

    private void OnDialogueEnd(DialogueNode node)
    {
        if (node == endNode)
        {
            PlayerPrefs.SetInt("powerPlugged", 1);
            SW.powerPlugged();
        }
    }
}
