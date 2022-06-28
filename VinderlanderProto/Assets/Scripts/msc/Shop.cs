using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject shopInterface;
    public GameObject player;

    public GameObject[] inventory = new GameObject[8];
    public GameObject shopInventoryDisplay;
    public GameObject emptyItem;


    public GameObject itemInstance;
    public GameObject content;
    public GameObject selectedObject;
    public PlayerInventory playerInventory;
    public GameController gameController;
    public Shop shop;

    private void Start()
    {
        RefillEmpty();
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    public void OnEnable()
    {
        RefillEmpty();
        Cursor.lockState = CursorLockMode.None;
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        for (int i = 0; i < content.transform.childCount; i++)
        {
            // Destroy(content.transform.GetChild(i).GetComponent<Image>());
            Destroy(content.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < inventory.Length; i++)
        {
                if (inventory[i].tag == "Item")
                {
                    GameObject newItemInstance;
                    newItemInstance = Instantiate(itemInstance);
                    newItemInstance.transform.SetParent(content.transform);
                    newItemInstance.GetComponent<UIItemHolder>().inWorldItem = inventory[i];
                    newItemInstance.GetComponent<Image>().sprite = inventory[i].GetComponent<InWorldItemContainer>().item.itemSprite;
                }
                else if (inventory[i].tag == "Empty")
                {
                    GameObject newItemInstance;
                    newItemInstance = Instantiate(itemInstance);
                    newItemInstance.transform.SetParent(content.transform);
                    newItemInstance.GetComponent<UIItemHolder>().inWorldItem = inventory[i];
                    newItemInstance.GetComponent<Image>().sprite = inventory[i].GetComponent<InWorldItemContainer>().item.itemSprite;
                }
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
        if (Vector3.Distance(transform.position, player.transform.position) <= 3)
        {
            shopInterface.gameObject.SetActive(true);
        }
        else
        {
            shopInterface.gameObject.SetActive(false);
        }
    }
}
