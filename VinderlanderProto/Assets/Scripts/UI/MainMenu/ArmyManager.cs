using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyManager : MonoBehaviour
{
    public GameObject gameController;
    public GameObject[] alliedArmy;
    public float Money;
    public float moneySpent;
    public Text fundDisplay;
    public Dropdown moneyOptions;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fundDisplay.text = "Funds: " + Money;
        if (moneyOptions.value == 0)
        {
            Money = 10000 - moneySpent;
        }
        else if (moneyOptions.value == 1)
        {
            Money = 8000 - moneySpent;
        }
        else if (moneyOptions.value == 2)
        {
            Money = 6000 - moneySpent;
        }
    }
}
