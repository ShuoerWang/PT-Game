using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject tool;

    public UISound UISound;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("InventoryPanel").GetComponent<Inventory>();
        UISound = GameObject.Find("UISound").GetComponent<UISound>();
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
                inventory.AddItem(tool);
                UISound.ItemCollect();
                Destroy(gameObject);
            }
        }
    }
}
