using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : MonoBehaviour
{

    public ToolSelect toolSelect;
    public Results results;
    public GameObject gelBlobPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && toolSelect.IsGel())
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] raycastHits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
            foreach (RaycastHit2D raycastHit in raycastHits)
            {
                if (raycastHit.collider != null && raycastHit.transform.parent != null && raycastHit.transform.parent.tag.Equals("foot"))
                {
                    Instantiate(gelBlobPrefab, worldPosition, Quaternion.identity);
                    results.SetGelPosition(worldPosition);
                    break;
                }
            }
        }
    }

}
