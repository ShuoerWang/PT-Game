using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public Collider2D foot;
    public Collider2D back;
    public Collider2D patient;
    public SWRoomManager sw;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 screenPosition = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            RaycastHit2D raycastHit0 = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (raycastHit0.collider == foot)
            {
                sw.MoveToFoot();
            }
            else if (raycastHit0.collider == back)
            {
                sw.MoveToBack();
            }
            else if (raycastHit0.collider == patient)
            {
                sw.MoveToPatient();
            }
        }      
    }
}
