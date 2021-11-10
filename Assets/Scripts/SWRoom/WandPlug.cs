using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandPlug : MonoBehaviour
{
    public SWRoomManager SW;
    public Treatment treatment;

    // Start is called before the first frame update
    void Start()
    {
        
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
                if (PlayerPrefs.GetInt("wandPlugged") == 0)
                {
                    if (PlayerPrefs.GetString("currentTool").Equals("wand"))
                    {
                        PlayerPrefs.SetInt("wandPlugged", 1);
                        SW.wandPlugged();
                    } else
                    {
                        treatment.triggerMachinePlugHint();
                    }
                }
            }
        }
    }
}
