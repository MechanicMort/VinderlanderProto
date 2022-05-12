using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{
    public GameObject bluePrintToPlace;
    public GameObject tempbluePrintToPlace;
    public GameObject buildingDisplay;
    public GameObject tempbuildingDisplay;
    public bool isBuildMode = false;
    public bool canBuild = false;
    // Start is called before the first frame update
    void Start()
    {
        tempbuildingDisplay = Instantiate(buildingDisplay);
    }

    // Update is called once per frame
    void Update()
    {
        if (canBuild && Input.GetKeyDown(KeyCode.Q))
        {
            isBuildMode = !isBuildMode;
        }
        if (isBuildMode)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            tempbuildingDisplay.transform.position = new Vector3(0, -300, 0);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (isBuildMode == true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000,LayerMask.GetMask("Ground")))
            {
                tempbuildingDisplay.transform.position = hit.point;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    tempbluePrintToPlace = Instantiate(bluePrintToPlace);
                    tempbluePrintToPlace.transform.position = hit.point;
                }
            }
            if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Building")))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    print("OnBuilding");
                    hit.transform.GetComponent<Building>().SendMessage("ActivateCanvas", true);
                }
            }
        } 
    }
}
