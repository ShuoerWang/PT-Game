using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
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
                if (PlayerPrefs.GetInt("powerPlugged") == 0)
                {
                    treatment.triggerMachinePowerHint();
                }
            }
        }
    }
}
