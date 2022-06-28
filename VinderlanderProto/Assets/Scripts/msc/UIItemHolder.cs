using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemHolder : MonoBehaviour
{
    public GameObject inWorldItem;
    public InventoryManager InventoryManager;
    public void SetSelectedItem()
    {
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        InventoryManager.selectedObject = inWorldItem;
    }
}
