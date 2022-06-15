using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[8];
    public GameObject playerInventoryDisplay;
    public GameObject emptyItem;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = Instantiate(emptyItem);
        }
    }

   public void RefillEmpty()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = Instantiate(emptyItem);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Item")
                {
                    for (int i = 0; i < inventory.Length; i++)
                    {
                        if (inventory[i]==null)
                        {
                            inventory[i] = Instantiate(emptyItem);
                        }
                        else if (inventory[i].tag == "Empty")
                        {
                            inventory[i] = hit.transform.gameObject;
                            hit.transform.gameObject.SetActive(false);
                            break;
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            playerInventoryDisplay.SetActive(!playerInventoryDisplay.activeInHierarchy);
        }
    }
}
