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


    void Start()
    {
        items = new GameObject[gameObject.transform.childCount];
        isFull = new bool[gameObject.transform.childCount];
        PlayerPrefs.SetInt("glove", 0);
        PlayerPrefs.SetInt("marker", 0);
        PlayerPrefs.SetInt("gel", 0);
        PlayerPrefs.SetInt("wand", 0);
        PlayerPrefs.SetString("currentTool", "");
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

    public void UpdateTool(string tagName) {
        if (tagName.Equals(TAG_GLOVE))
        {
            Debug.Log("glove!");
            currentTool = TOOL_GLOVE;
            PlayerPrefs.SetString("currentTool", TAG_GLOVE);
        }
        else if (tagName.Equals(TAG_MARKER))
        {
            Debug.Log("marker!");
            currentTool = TOOL_MARKER;
            PlayerPrefs.SetString("currentTool", TAG_MARKER);
        }
        else if (tagName.Equals(TAG_GEL))
        {
            Debug.Log("gel!");
            currentTool = TOOL_GEL;
            PlayerPrefs.SetString("currentTool", TAG_GEL);
        }
        else if (tagName.Equals(TAG_WAND))
        {
            Debug.Log("wand!");
            currentTool = TOOL_WAND;
            PlayerPrefs.SetString("currentTool", TAG_WAND);
        }
        DisplayCurrentTool();
    }

    private void DisplayCurrentTool()
    {
        HighlightCurrentTool();
        switch (currentTool)
        {
            case TOOL_GLOVE:
                cursorSpriteRenderer.sprite = gloveSprite;
                cursorSpriteRenderer.transform.localPosition = new Vector2(0.3f, -0.3f);
                break;
            case TOOL_GEL:
                cursorSpriteRenderer.sprite = gelSprite;
                cursorSpriteRenderer.transform.localPosition = new Vector2(0.0f, -0.5f);
                break;
            case TOOL_MARKER:
                cursorSpriteRenderer.sprite = markerSprite;
                cursorSpriteRenderer.transform.localPosition = new Vector2(0.3f, 0.0f);
                break;
            case TOOL_WAND:
                cursorSpriteRenderer.sprite = wandSprite;
                cursorSpriteRenderer.transform.localPosition = new Vector2(0.1f, -0.7f);
                break;
            default:
                break;
        }
    }

    public void HighlightCurrentTool()
    {
        cursorSpriteRenderer.color = Color.white;
    }

    public void UpdatePlayerPrefs(string tagName)
    {
        if (tagName.Equals(TAG_GLOVE))
        {
            PlayerPrefs.SetInt("glove", 0);
        }
        else if (tagName.Equals(TAG_MARKER))
        {
            PlayerPrefs.SetInt("marker", 0);
        }
        else if (tagName.Equals(TAG_GEL))
        {
            PlayerPrefs.SetInt("gel", 0);
        }
        else if (tagName.Equals(TAG_WAND))
        {
            PlayerPrefs.SetInt("wand", 0);
        }
    }
}
