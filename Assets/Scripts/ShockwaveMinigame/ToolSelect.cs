using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolSelect : MonoBehaviour
{

    private const int TOOL_GLOVE = 0;
    private const int TOOL_MARKER = 1;
    private const int TOOL_GEL = 2;
    private const int TOOL_WAND = 3;
    private const string TAG_GLOVE = "glove";
    private const string TAG_MARKER = "marker";
    private const string TAG_GEL = "gel";
    private const string TAG_WAND = "wand";

    public Sprite gloveSprite;
    public Sprite markerSprite;
    public Sprite gelSprite;
    public Sprite wandSprite;
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    public GameObject wandDisplay;
    public Machine machine;
    public Wand wand;
    public SpriteRenderer cursorSpriteRenderer;

    private int currentTool = -1;
    private bool toolsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toolsActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D raycastHit = Physics2D.Raycast(worldPosition, Vector2.zero);
                if (raycastHit.collider != null)
                {
                    string tagName = raycastHit.transform.tag;
                    if (tagName.Equals(TAG_GLOVE))
                    {
                        currentTool = TOOL_GLOVE;
                        dialogueBox.SetActive(true);
                        dialogueText.text = "";
                        wandDisplay.SetActive(false);
                    }
                    else if (tagName.Equals(TAG_MARKER))
                    {
                        currentTool = TOOL_MARKER;
                        dialogueBox.SetActive(false);
                        wandDisplay.SetActive(false);
                    }
                    else if (tagName.Equals(TAG_GEL))
                    {
                        currentTool = TOOL_GEL;
                        dialogueBox.SetActive(false);
                        wandDisplay.SetActive(false);
                    }
                    else if (tagName.Equals(TAG_WAND))
                    {
                        currentTool = TOOL_WAND;
                        dialogueBox.SetActive(false);
                        wandDisplay.SetActive(true);
                        wand.SetupWand();
                    }
                }
                DisplayCurrentTool();
            }
        }
    }

    public void DisableTools()
    {
        currentTool = -1;
        toolsActive = false;
    }

    public bool IsGlove()
    {
        return currentTool == TOOL_GLOVE;
    }

    public bool IsMarker()
    {
        return currentTool == TOOL_MARKER;
    }

    public bool IsGel()
    {
        return currentTool == TOOL_GEL;
    }

    public bool IsWand()
    {
        return currentTool == TOOL_WAND;
    }

    private void DisplayCurrentTool()
    {
        HighlightCurrentTool();
        if (IsGlove())
        {
            cursorSpriteRenderer.sprite = gloveSprite;
            cursorSpriteRenderer.transform.localPosition = new Vector2(0.75f, -0.75f);
        }
        else if (IsMarker())
        {
            cursorSpriteRenderer.sprite = markerSprite;
            cursorSpriteRenderer.transform.localPosition = new Vector2(0.75f, 0.0f);
        }
        else if (IsGel())
        {
            cursorSpriteRenderer.sprite = gelSprite;
            cursorSpriteRenderer.transform.localPosition = new Vector2(0.0f, -1.25f);
        }
        else if (IsWand())
        {
            cursorSpriteRenderer.sprite = wandSprite;
            cursorSpriteRenderer.transform.localPosition = new Vector2(0.25f, -1.6f);
            if (!wand.IsWandOn())
            {
                DimCurrentTool();
            }
        }
    }

    public void DimCurrentTool()
    {
        cursorSpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    }

    public void HighlightCurrentTool()
    {
        cursorSpriteRenderer.color = Color.white;
    }

}
