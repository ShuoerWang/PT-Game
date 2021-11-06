using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{

    public ToolSelect toolSelect;
    public Results results;
    public GameObject markerBlobPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && toolSelect.IsMarker())
        {
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] raycastHits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
            bool onGel = false;
            foreach (RaycastHit2D raycastHit in raycastHits)
            {
                if (raycastHit.collider != null && raycastHit.transform.tag.Equals("gelBlob"))
                {
                    onGel = true;
                }
            }
            if (!onGel)
            {
                foreach (RaycastHit2D raycastHit in raycastHits)
                {
                    if (raycastHit.collider != null && raycastHit.transform.parent != null && raycastHit.transform.parent.tag.Equals("foot"))
                    {
                        Instantiate(markerBlobPrefab, worldPosition, Quaternion.identity);
                        break;
                    }
                }
            }
        }
    }

}
