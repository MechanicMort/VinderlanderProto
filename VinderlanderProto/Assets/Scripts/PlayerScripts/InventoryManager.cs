using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemInstance;
    public GameObject content;
    public GameObject selectedObject;
    public PlayerInventory playerInventory;
    public GameController gameController;
    public Shop shop;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }



    public void DropItem()
    {
        if (selectedObject != null)
        {
            if (selectedObject.tag != "Empty")
            {
                selectedObject.transform.position = player.transform.position;
                selectedObject.SetActive(true);
                for (int i = 0; i < playerInventory.inventory.Length; i++)
                {
                    if (playerInventory.inventory[i] == selectedObject)
                    {
                        selectedObject = null;
                        playerInventory.inventory[i] = null;
                        playerInventory.RefillEmpty();
                        OnEnable();
                        break;

                    }
                }

            }
        }
       

    }

    public void AddItem(GameObject item)
    {
        if (item.tag == "Item")
        {
            for (int i = 0; i < playerInventory.inventory.Length; i++)
            {
              
                if (playerInventory.inventory[i].tag == "Empty")
                {
                    playerInventory.inventory[i] = item;
                    item.transform.gameObject.SetActive(false);
                    break;
                }
            }
        }

    }
    public void PurchaseItem()
    {
        if (selectedObject.tag == "Item")
        {
            if (gameController.money >= selectedObject.GetComponent<InWorldItemContainer>().item.worth)
            {
                for (int i = 0; i < playerInventory.inventory.Length; i++)
                {
                    if (playerInventory.inventory[i].tag == "Empty")
                    {
                        for (int x = 0; x < shop.inventory.Length; x++)
                        {
                            print(shop.inventory[x]);
                            if (shop.inventory[x] == selectedObject)
                            {
                                playerInventory.inventory[i] = selectedObject;
                                selectedObject.transform.gameObject.SetActive(false);
                                gameController.money -= selectedObject.GetComponent<InWorldItemContainer>().item.worth;

                                selectedObject = null;
                                shop.inventory[x] = null;
                                shop.RefillEmpty();
                                shop.OnEnable();
                                OnEnable();
                                break;
                            }
                        }
                        break;
                    }
                }
            }
          
        }

    }
    public void SellItem()
    {
        if (selectedObject != null)
        {

            if (selectedObject.tag != "Empty")
            {
                for (int i = 0; i < playerInventory.inventory.Length; i++)
                {
                    if (playerInventory.inventory[i] == selectedObject)
                    {
                        gameController.money += playerInventory.inventory[i].GetComponent<InWorldItemContainer>().item.worth;
                        selectedObject = null;
                        playerInventory.inventory[i] = null;
                        playerInventory.RefillEmpty();
                        OnEnable();
                        break;
                    }
                }
            }
        }

            
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        for (int i = 0; i < content.transform.childCount; i++)
        {
             // Destroy(content.transform.GetChild(i).GetComponent<Image>());
              Destroy(content.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < playerInventory.inventory.Length; i++)
        {
            if (playerInventory.inventory[i] != null)
            {
                if (playerInventory.inventory[i].tag == "Item")
                {
                    GameObject newItemInstance;
                    newItemInstance = Instantiate(itemInstance);
                    newItemInstance.transform.SetParent(content.transform);
                    newItemInstance.GetComponent<UIItemHolder>().inWorldItem = playerInventory.inventory[i];
                    newItemInstance.GetComponent<Image>().sprite = playerInventory.inventory[i].GetComponent<InWorldItemContainer>().item.itemSprite;
                }
                else if (playerInventory.inventory[i].tag == "Empty")
                {
                    GameObject newItemInstance;
                    newItemInstance = Instantiate(itemInstance);
                    newItemInstance.transform.SetParent(content.transform);
                    newItemInstance.GetComponent<UIItemHolder>().inWorldItem = playerInventory.inventory[i];
                    newItemInstance.GetComponent<Image>().sprite = playerInventory.inventory[i].GetComponent<InWorldItemContainer>().item.itemSprite;
                }
            }    
        }
    }
}
