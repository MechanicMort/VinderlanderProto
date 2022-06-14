using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InWorldItemContainer : MonoBehaviour
{
    public Canvas pickUpIndicator;
    public Item item;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Update()
    {
        if (Vector3.Distance(transform.position,player.transform.position) <=3)
        {
            pickUpIndicator.gameObject.SetActive(true);
        }
        else
        {
            pickUpIndicator.gameObject.SetActive(false);
        }
    }
}
