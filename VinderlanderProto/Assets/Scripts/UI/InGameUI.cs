using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject movementOrders;
    public GameObject rotationOrders;
    public GameObject formationOrders;
    public GameObject mscOrders;  
    public GameObject baseOrders;  
    public Text movementOrdersTxt;
    public Text rotationOrdersTxt;
    public Text formationOrdersTxt;
    public Text mscOrdersTxt;
    public List<GameObject> SelectedUnits;
    public List<GameObject> AttackUnits;
    public List<GameObject> unitCards;

    public GameObject unitCard;
    public GameObject UnitCardTray;


    private Vector3 moveTo;

    public Camera playerCam;

    public GameObject goingTo;

    private bool isMenu;

    private Vector3 order;
    // Start is called before the first frame update
    void Start()
    {
        movementOrders.SetActive(false);
        rotationOrders.SetActive(false);
        formationOrders.SetActive(false);
        mscOrders.SetActive(false);
        baseOrders.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Orders();
        SoundTheHorn();
        AddUnits();
    }




    public void UnitCards() {
        for (int i = 0; i < UnitCardTray.gameObject.transform.childCount; i++)
        {
          Destroy(UnitCardTray.gameObject.transform.GetChild(i).gameObject);
        }
   
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            GameObject UnitCardTemp;
      
            UnitCardTemp = Instantiate(SelectedUnits[i].GetComponent<FormationManager>().UnitCard);
            UnitCardTemp.GetComponent<UnitCard>().FormationManager = SelectedUnits[i];
            UnitCardTemp.transform.SetParent(UnitCardTray.transform,true);
           
        }
    }

    public void SoundTheHorn()
    {
        if (Input.GetKeyDown(KeyCode.B)) 
        {
            for (int i = 0; i < SelectedUnits.Count; i++)
            {
                SelectedUnits[i].GetComponent<FormationManager>().DoOrders();
            }
        }
       
        
    }



    public void AddUnits()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            int layerMask = 1 << 7;

            RaycastHit hit;
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100000, layerMask))
            {

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.tag == "Formation")
                {
                    if (hit.collider.gameObject.transform.parent != null)
                    {
                        if (!SelectedUnits.Contains(hit.collider.gameObject.transform.parent.gameObject))
                        {
                            SelectedUnits.Add(hit.collider.gameObject.transform.parent.gameObject);
                            UnitCards();
                        }
                    }
                  

                }
            }
        }
        else if (Input.GetKey(KeyCode.Mouse2))
        {
            int layerMask = 1 << 7;

            RaycastHit hit;
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100000,  layerMask))
            {

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.tag == "Formation")
                {
                    if (hit.collider.gameObject.transform.parent != null)
                    {
                        if (SelectedUnits.Contains(hit.collider.gameObject.transform.parent.gameObject))
                        {
                            SelectedUnits.Remove(hit.collider.gameObject.transform.parent.gameObject);
                            UnitCards();
                        }
                    }


                }
            }
        }
    }





    public void Orders()
    {


        

       // CheckActive();
        if (movementOrders.activeInHierarchy == true)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isMenu = false;
                movementOrdersTxt.text = "No Order Currently Selected:";
                movementOrders.SetActive(false);
                baseOrders.SetActive(true);
            }
    
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                movementOrdersTxt.text += "";
                string orderType = "MoveTo";
                int layerMask = 1 << 9;


                RaycastHit hit;

                Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
                if (screenRect.Contains(Input.mousePosition))
                {
                    if (playerCam != null)
                    {
                        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit, layerMask, 1000))
                        {
                            // If the raycast hit a GameObject...
                            moveTo = hit.point;
                            SelectedUnits[0].GetComponent<FormationManager>().ReceiveOrder(hit.point, orderType);
                        }
                    }
                }
                for (int i = 0; i < SelectedUnits.Count-1; i++)
                {
                    Vector3 relativeDistance = SelectedUnits[i].transform.position - SelectedUnits[i+1].transform.position;
                    moveTo -= relativeDistance;
                    SelectedUnits[i+1].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                movementOrdersTxt.text += "";
                string orderType = "Attack";
                int layerMask = 1 << 11;

                RaycastHit hit;
                Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    if (hit.transform.tag == "EnemyFormation")
                    {
                        GameObject moveTo = hit.transform.gameObject;
                        movementOrdersTxt.text = "Orders are :" + "";

                        for (int i = 0; i < SelectedUnits.Count; i++)
                        {
                            SelectedUnits[i].GetComponent<FormationManager>().ReceiveAttackOrder(moveTo, orderType);
                        }
                    }
                }

            }

            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                movementOrdersTxt.text += "";
                string orderType = "Advance";

                moveTo = new Vector3(0,0,10);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                movementOrdersTxt.text += "";
                string orderType = "Advance";

                moveTo = new Vector3(0, 0, -10);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }  else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                string orderType = "Run";

                moveTo = new Vector3(0, 0, 0);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
        }

        if (formationOrders.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isMenu = false;
                formationOrdersTxt.text = "No Order Currently Selected:";
                baseOrders.SetActive(true);
                formationOrders.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    Vector3 Formation;
                    Formation = new Vector3(1, 0, 0);
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(Formation, "FormationChange");
                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    Vector3 Formation;
                    Formation = new Vector3(2, 0, 0);
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(Formation, "FormationChange");
                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    Vector3 Formation;
                    Formation = new Vector3(3, 0, 0);
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(Formation, "FormationChange");
                }

            }
        }

        if (rotationOrders.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isMenu = false;
                rotationOrdersTxt.text = "No Order Currently Selected:";
                rotationOrders.SetActive(false);
                baseOrders.SetActive(true);
            }
        
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                string orderType = "LookAt";
                int layerMask = 1 << 9;

                RaycastHit hit;
                Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, layerMask, 1000))
                   {
                        moveTo = hit.point;
                   }

                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                string orderType = "Rotate";
                moveTo = new Vector3(0,15,0);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                string orderType = "Rotate";
                moveTo = new Vector3(0, -15, 0);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                string orderType = "Rotate";
                moveTo = new Vector3(0, 180, 0);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }

        }

        if (mscOrders.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isMenu = false;
                mscOrdersTxt.text = "No Order Currently Selected:";
                mscOrders.SetActive(false);
                baseOrders.SetActive(true); 
            }

        }



        // DoOrder();

        // 1 open movement orders 
        if (Input.GetKeyDown(KeyCode.Alpha1) && isMenu == false)
        {
            isMenu = true;
            movementOrders.SetActive(true);
            baseOrders.SetActive(false);
        }        
        else if (Input.GetKeyDown(KeyCode.Alpha2) && isMenu == false)
        {
            isMenu = true;
            rotationOrders.SetActive(true);
            baseOrders.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && isMenu == false)
        {
            isMenu = true;
            formationOrders.SetActive(true);
            baseOrders.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && isMenu == false)
        {
            isMenu = true;
            mscOrders.SetActive(true);
            baseOrders.SetActive(false);
        }
        
    }
}
