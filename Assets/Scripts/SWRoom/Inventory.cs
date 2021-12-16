using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    private bool[] isFull;
    private GameObject[] items;
    private const int TOOL_GLOVE = 0;
    private const int TOOL_MARKER = 1;
    private const int TOOL_GEL = 2;
    private const int TOOL_WAND = 3;
    private const string TAG_GLOVE = "glove";
    private const string TAG_MARKER = "marker";
    private const string TAG_GEL = "gel";
    private const string TAG_WAND = "wand";

    private int currentTool = -1;

    public Sprite gloveSprite;
    public Sprite markerSprite;
    public Sprite gelSprite;
    public Sprite wandSprite;
    public SpriteRenderer cursorSpriteRenderer;

    public GameObject wandDisplay;
    public WandDisplay wand;

    void Start()
    {
        items = new GameObject[gameObject.transform.childCount];
        isFull = new bool[gameObject.transform.childCount];
        
    }


    public bool AddItem(GameObject tool)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (!isFull[i])
            {
                isFull[i] = true;
                GameObject newObject = Instantiate(tool,
                    gameObject.transform.GetChild(i).transform, false);
                items[i] = newObject;
                UpdatePlayerPrefs(tool.tag);
                return true;
            }
        }
        return false;
    }

    public void ResetGame()
    {
        cleanCurrentTool();
        wandDisplay.SetActive(false);
        currentTool = -1;
        PlayerPrefs.SetString("currentTool", "");
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (isFull[i])
            {
                isFull[i] = false;
                Destroy(gameObject.transform.GetChild(i).transform.GetChild(0).gameObject);
            }
                
        }
    }

    public void UpdateTool(string tagName) {
        if (tagName.Equals(PlayerPrefs.GetString("currentTool")))
        {
            currentTool = -1;
            PlayerPrefs.SetString("currentTool", "");
            wandDisplay.SetActive(false);
        }
        else if (tagName.Equals(TAG_GLOVE))
        {
            currentTool = TOOL_GLOVE;
            PlayerPrefs.SetString("currentTool", TAG_GLOVE);
            wandDisplay.SetActive(false);
        }
        else if (tagName.Equals(TAG_MARKER))
        {
            currentTool = TOOL_MARKER;
            PlayerPrefs.SetString("currentTool", TAG_MARKER);
            wandDisplay.SetActive(false);
        }
        else if (tagName.Equals(TAG_GEL))
        {
            currentTool = TOOL_GEL;
            PlayerPrefs.SetString("currentTool", TAG_GEL);
            wandDisplay.SetActive(false);
        }
        else if (tagName.Equals(TAG_WAND))
        {
            currentTool = TOOL_WAND;
            PlayerPrefs.SetString("currentTool", TAG_WAND);
            wandDisplay.SetActive(true);
        }
        DisplayCurrentTool();
    }

    private void DisplayCurrentTool()
    {
        DimCurrentTool();
        switch (currentTool)
        {
            case TOOL_GLOVE:
                cursorSpriteRenderer.sprite = gloveSprite;
                cursorSpriteRenderer.transform.localPosition = new Vector2(1.5f, -1.5f);
                break;
            case TOOL_GEL:
                cursorSpriteRenderer.sprite = gelSprite;
                cursorSpriteRenderer.transform.localPosition = new Vector2(0.0f, -2.3f);
                break;
            case TOOL_MARKER:
                cursorSpriteRenderer.sprite = markerSprite;
                cursorSpriteRenderer.transform.localPosition = new Vector2(1.3f, 0.0f);
                break;
            case TOOL_WAND:
                cursorSpriteRenderer.sprite = wandSprite;
                cursorSpriteRenderer.transform.localPosition = new Vector2(0.43f, -3.25f);
                if (!wand.IsWandOn())
                {
                    DimCurrentTool();
                }
                break;
            default:
                cursorSpriteRenderer.sprite = null;
                break;
        }
    }

    public void HighlightCurrentTool()
    {
        cursorSpriteRenderer.color = Color.white;
    }

    public void DimCurrentTool()
    {
        cursorSpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
    }

    public void UpdatePlayerPrefs(string tagName)
    {
        if (tagName.Equals(TAG_GLOVE))
        {
            PlayerPrefs.SetInt("glove", 1);
        }
        else if (tagName.Equals(TAG_MARKER))
        {
            PlayerPrefs.SetInt("marker", 1);
        }
        else if (tagName.Equals(TAG_GEL))
        {
            PlayerPrefs.SetInt("gel", 1);
        }
        else if (tagName.Equals(TAG_WAND))
        {
            PlayerPrefs.SetInt("wand", 1);
        }
    }

    public void DisableTools()
    {
        currentTool = -1;
        PlayerPrefs.SetString("currentTool", "");
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

    public void cleanCurrentTool()
    {
        cursorSpriteRenderer.sprite = null;
    }
}
