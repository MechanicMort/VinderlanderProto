using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public Canvas buildingCanvas;
    public float health;
    public Dictionary<string, float> storageCapacity = new Dictionary<string, float>();
    public float woodStorage;
    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.tag) {
            case "StorageBuilding":print("");break; 
        
        }

            

    }


    //    buildCost.Add("Wood", woodCost);
    //    buildCost.Add("Stone", stoneCost);
    //    buildCost.Add("Metal", metalCost);
    //    buildCost.Add("Work", workCost); 
    // Update is called once per frame


    public void ActivateCanvas(bool shouldActive)
    {
        buildingCanvas.gameObject.SetActive(!buildingCanvas.isActiveAndEnabled);
    }

    void Update()
    {

    }

}
