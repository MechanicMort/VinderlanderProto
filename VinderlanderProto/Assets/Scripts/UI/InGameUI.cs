using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUI : MonoBehaviour
{
    public Image playerHpDisplay;
    public GameObject player;
   
    public GameObject movementOrders;
    public GameObject rotationOrders;
    public GameObject formationOrders;
    public GameObject mscOrders;  
    public GameObject baseOrders;  
    public TextMeshProUGUI movementOrdersTxt;
    public TextMeshProUGUI rotationOrdersTxt;
    public TextMeshProUGUI formationOrdersTxt;
    public TextMeshProUGUI mscOrdersTxt;
    public List<GameObject> SelectedUnits;
    public List<GameObject> AttackUnits;
    public List<GameObject> unitCards;

    public GameObject unitCard;
    public GameObject UnitCardTray;
    public GameObject UnitCardTrayContent;


    private Vector3 moveTo;

    public Camera playerCam;

    public GameObject goingTo;

    private bool isMenu;

    private Vector3 order;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movementOrders.SetActive(false);
        rotationOrders.SetActive(false);
        formationOrders.SetActive(false);
        mscOrders.SetActive(false);
        baseOrders.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        playerHpDisplay.fillAmount = player.GetComponent<CombatController>().hp / 100;
        Orders();
        SoundTheHorn();
        AddUnits();
    }




    public void UnitCards() {
        for (int i = 0; i < UnitCardTrayContent.gameObject.transform.childCount; i++)
        {
          Destroy(UnitCardTrayContent.gameObject.transform.GetChild(i).gameObject);
        }
   
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            GameObject UnitCardTemp;
      
            UnitCardTemp = Instantiate(SelectedUnits[i].GetComponent<FormationManager>().UnitCard);
            UnitCardTemp.GetComponent<UnitCard>().FormationManager = SelectedUnits[i];
            UnitCardTemp.transform.SetParent(UnitCardTrayContent.transform,true);
           
        }
    }

    public void SoundTheHorn()
    {
        if (Input.GetKeyDown(KeyCode.O)) 
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
            RaycastHit hit;
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Formations")))
            {

                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.tag == "Formation")
                {
                    if (!SelectedUnits.Contains(hit.collider.gameObject))
                    {
                        SelectedUnits.Add(hit.collider.gameObject);
                        UnitCards();
                    }
             
                }
            }
            else if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Default")))
            {
                //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.tag == "Person")
                {
                    if (hit.collider.gameObject.GetComponent<PawnController>().Formation != null)
                    {
                        if (SelectedUnits.Contains(hit.collider.gameObject.GetComponent<PawnController>().Formation))
                        {
                            SelectedUnits.Add(hit.collider.gameObject.GetComponent<PawnController>().Formation);
                            UnitCards();
                        }
                    }


                }
            }
        }
        else if (Input.GetKey(KeyCode.Mouse2))
        {
            RaycastHit hit;
            Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Formations")))
            {
                // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.tag == "Formation")
                {
                       if (SelectedUnits.Contains(hit.collider.gameObject))
                        {
                            SelectedUnits.Remove(hit.collider.gameObject);
                            UnitCards();
                        }                   
                }
            }
            else if (Physics.Raycast(ray, out hit, 100000, LayerMask.GetMask("Default")))
            {
              //  Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider.gameObject.tag == "Person")
                {
                    if (hit.collider.gameObject.GetComponent<PawnController>().Formation != null)
                    {
                        if (SelectedUnits.Contains(hit.collider.gameObject.GetComponent<PawnController>().Formation))
                        {
                            SelectedUnits.Remove(hit.collider.gameObject.GetComponent<PawnController>().Formation);
                            UnitCards();
                        }
                    }


                }
            }
        }
    }


    public void Orders()
    {
        if (movementOrders.activeInHierarchy == true && isMenu == true)
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
                movementOrdersTxt.text = "Movement Order Set:";
                string orderType = "MoveTo";
                RaycastHit hit;

                Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, LayerMask.GetMask("Formations"), 1000))
                {
                    moveTo = hit.point;
                    for (int i = 0; i < SelectedUnits.Count; i++)
                    {
                        if (i > 0)
                        {
                            Vector3 relativeDistance = SelectedUnits[i - 1].transform.position - SelectedUnits[i].transform.position;
                            moveTo -= relativeDistance;
                        }
                        SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                    }

                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                string orderType = "Attack";
                int layerMask = 1 << 11;

                RaycastHit hit;
                Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 10000, layerMask))
                {
                    if (hit.transform.tag == "EnemyFormation")
                    {
                        movementOrdersTxt.text += "Attack Order Set:";
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
                movementOrdersTxt.text += "Advance Order Set:";
                string orderType = "Advance";

                moveTo = new Vector3(0, 0, 10);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                movementOrdersTxt.text += "Retreat Order Set:";
                string orderType = "Advance";

                moveTo = new Vector3(0, 0, -10);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                movementOrdersTxt.text += "Run Order Set:";
                string orderType = "Run";

                moveTo = new Vector3(0, 0, 0);
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(moveTo, orderType);
                }
            }
        }
        else if (rotationOrders.activeInHierarchy == true && isMenu == true)
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
                moveTo = new Vector3(0, 15, 0);
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
        else if (formationOrders.activeInHierarchy == true && isMenu == true)
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
                    formationOrdersTxt.text = "Changing Formation to:...";
                    Vector3 Formation;
                    Formation = new Vector3(1, 0, 0);
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(Formation, "FormationChange");
                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    formationOrdersTxt.text = "Changing Formation to:...";
                    Vector3 Formation;
                    Formation = new Vector3(2, 0, 0);
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(Formation, "FormationChange");
                }

            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                for (int i = 0; i < SelectedUnits.Count; i++)
                {
                    formationOrdersTxt.text = "Changing Formation to:...";
                    Vector3 Formation;
                    Formation = new Vector3(3, 0, 0);
                    SelectedUnits[i].GetComponent<FormationManager>().ReceiveOrder(Formation, "FormationChange");
                }

            }
        }

        else if (mscOrders.activeInHierarchy == true && isMenu == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isMenu = false;
                mscOrdersTxt.text = "No Order Currently Selected:";
                mscOrders.SetActive(false);
                baseOrders.SetActive(true);
            }

        }


        // close orders
        else  if (Input.GetKeyDown(KeyCode.Escape) && isMenu == false && baseOrders.activeInHierarchy)
        {
             baseOrders.SetActive(false);
             UnitCardTray.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F1) && isMenu == false && !baseOrders.activeInHierarchy)
        {
            isMenu = false;
            baseOrders.SetActive(true);
            UnitCardTray.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && isMenu == false && baseOrders.activeInHierarchy)
        {
            isMenu = true;
            movementOrders.SetActive(true);
            baseOrders.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && isMenu == false && baseOrders.activeInHierarchy)
        {
            isMenu = true;
            rotationOrders.SetActive(true);
            baseOrders.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && isMenu == false && baseOrders.activeInHierarchy)
        {
            isMenu = true;
            formationOrders.SetActive(true);
            baseOrders.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && isMenu == false && baseOrders.activeInHierarchy)
        {
            isMenu = true;
            mscOrders.SetActive(true);
            baseOrders.SetActive(false);
        }





 
        
    }
}
