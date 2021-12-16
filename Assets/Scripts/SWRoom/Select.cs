using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    private Inventory Inventory;
    private UISound UISound;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = GameObject.Find("InventoryPanel").GetComponent<Inventory>();
        UISound = GameObject.Find("UISound").GetComponent<UISound>();
    }

    public void OnSelect()
    {
        Inventory.UpdateTool(gameObject.tag);
        UISound.ItemCollect();
    }
}
