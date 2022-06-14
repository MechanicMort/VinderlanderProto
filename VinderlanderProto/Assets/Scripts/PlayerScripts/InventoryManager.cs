using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemInstance;
    public GameObject content;
    public PlayerInventory playerInventory;

    private void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        for (int i = 0; i < playerInventory.inventory.Length; i++)
        {
            if (playerInventory.inventory[i].tag == "Item")
            {
                GameObject newItemInstance;
                newItemInstance  = Instantiate(itemInstance);
                newItemInstance.transform.SetParent(content.transform);
                print("Adding new instance");
            }
        }
    }
}
