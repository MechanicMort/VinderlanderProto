using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemHolder : MonoBehaviour
{
    public GameObject inWorldItem;
    public InventoryManager InventoryManager;
    public Button button;

    private void Start()
    {
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }
    public void SetSelectedItem()
    {
        InventoryManager.selectedObject = inWorldItem;
    }
}
