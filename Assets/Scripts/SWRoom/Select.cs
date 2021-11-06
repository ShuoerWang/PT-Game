using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    private Inventory Inventory;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("InventoryPanel").GetComponent<Inventory>();
    }

    public void OnSelect()
    {
        Inventory.UpdateTool(gameObject.tag);
    }
}
