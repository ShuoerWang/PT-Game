using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pointer : MonoBehaviour
{
    public GameObject Hint;
    public RectTransform HintBox;
    public TextMeshProUGUI HintText;

    // Start is called before the first frame update
    void Start()
    {
        Camera.main.aspect = 16.0f / 9.0f;
        Hint.transform.localPosition = new Vector2(0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D raycastHit = Physics2D.Raycast(mousePosition, Vector2.zero);
        

        if (raycastHit)
        {
            Hint.SetActive(true);
            string tagName = raycastHit.transform.tag;
            if (tagName.Equals("glove"))
            {
                HintText.text = "glove";
            }
            else if (tagName.Equals("marker"))
            {
                HintText.text = "marker";
            }
            else if (tagName.Equals("gel"))
            {
                HintText.text = "gel";
            }
            else if (tagName.Equals("wand"))
            {
                HintText.text = "wand";
            }
            else if (tagName.Equals("endButton"))
            {
                HintText.text = "end treatment";
            }
            else if (tagName.Equals("powerButton"))
            {
                HintText.text = "power button";
            }
            else if (tagName.Equals("powerPlug"))
            {
                HintText.text = "power plug";
            }
            else if (tagName.Equals("wandPlug"))
            {
                HintText.text = "wand plug";
            }
            else
            {
                Hint.SetActive(false);
            }
        } else
        {
            Hint.SetActive(false);
        }

        
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -5);
        HintBox.position = new Vector3(mousePosition.x, mousePosition.y + 0.5f, -5); 

    }

}