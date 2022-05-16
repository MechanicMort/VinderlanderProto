using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBuilding : MonoBehaviour
{
    public List<Vector3> waypoints = new List<Vector3>();
    public List<GameObject> walls = new List<GameObject>();
    private int currentWaypoint = -1;

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
                if (waypoints.Count > 0)
                {
                    tempbuildingDisplay.transform.position = waypoints[currentWaypoint];
                    tempbuildingDisplay.transform.localScale = new Vector3(1, 7, Vector3.Distance(waypoints[currentWaypoint], hit.point));
                    tempbuildingDisplay.transform.LookAt(Vector3.MoveTowards(waypoints[currentWaypoint], hit.point, Vector3.Distance(waypoints[currentWaypoint], hit.point) / 2));
                    tempbuildingDisplay.transform.position = Vector3.MoveTowards(waypoints[currentWaypoint], hit.point, Vector3.Distance(waypoints[currentWaypoint], hit.point) / 2);
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (waypoints.Count > 1)
                    {

                        GameObject tempbuildingDisplay2 = new GameObject();
                        walls.Add(tempbuildingDisplay2);
                        for (int i = 0; i < waypoints.Count - 1; i++)
                        {
                            tempbuildingDisplay2 = Instantiate(buildingDisplay);
                            tempbuildingDisplay2.transform.position = waypoints[i];
                            tempbuildingDisplay2.transform.localScale = new Vector3(1, 7, Vector3.Distance(waypoints[i], waypoints[i + 1]));
                            tempbuildingDisplay2.transform.LookAt(Vector3.MoveTowards(waypoints[i], waypoints[i + 1], Vector3.Distance(waypoints[i], waypoints[i + 1]) / 2));
                            tempbuildingDisplay2.transform.position = Vector3.MoveTowards(waypoints[i], waypoints[i + 1], Vector3.Distance(waypoints[i], waypoints[i + 1]) / 2);
                        }
                    }
                    waypoints.Add(hit.point);
                    currentWaypoint += 1;
                  //  tempbluePrintToPlace = Instantiate(bluePrintToPlace);
                   // tempbluePrintToPlace.transform.position = hit.point;
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
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                currentWaypoint = -1;
                if (waypoints.Count > 1)
                {
                    //placebuildings
                    GameObject theBuilding = new GameObject();
                    for (int i = 0; i < waypoints.Count - 1; i++)
                    {
                        theBuilding = Instantiate(bluePrintToPlace);
                        theBuilding.transform.position = waypoints[i];
                        theBuilding.transform.localScale = new Vector3(1, 1, Vector3.Distance(waypoints[i], waypoints[i + 1]));
                        theBuilding.transform.LookAt(Vector3.MoveTowards(waypoints[i], waypoints[i + 1], Vector3.Distance(waypoints[i], waypoints[i + 1]) / 2));
                        theBuilding.transform.position = Vector3.MoveTowards(waypoints[i], waypoints[i + 1], Vector3.Distance(waypoints[i], waypoints[i + 1]) / 2);
                    }
                    for (int i = 0; i < walls.Count; i++)
                    {
                        walls[i].SetActive(false);
                    }
                    waypoints.Clear();
                }
            }
        } 
    }
}
