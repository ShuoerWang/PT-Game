using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    // Start is called before the first frame update
    public SWRoomManager sWRoomManager;
    public Treatment treatment;

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
                if (PlayerPrefs.GetInt("powerPlugged") == 1)
                {
                    if (PlayerPrefs.GetInt("machineOn") == 0)
                    {
                        PlayerPrefs.SetInt("machineOn", 1);
                        sWRoomManager.changeToMO();
                    } else
                    {
                        PlayerPrefs.SetInt("machineOn", 0);
                        sWRoomManager.changeToMOFF();
                    }
                    
                }
                else if (PlayerPrefs.GetInt("powerPlugged") == 0
                    && PlayerPrefs.GetInt("machineOn") == 0)
                {
                    treatment.triggerMachineHint();
                }
            }
        }
    }
}
