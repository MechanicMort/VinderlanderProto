using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FormationManager : MonoBehaviour
{


    public float UnitSize;
    public float unitDepth;
    public float unitWidth;
    public int iDone;

    public List<GameObject> UnitManaged;
    public GameObject UnitPawn;
    public GameObject FormationShape;

    public GameObject goingTo;
    public GameObject Forward;

    public GameObject Player;


    private GameObject goingToTemp;

    private GameObject Flag;

    public float currentShape;

    private float Morale;

    private bool isBroken;

    public float CurrentFormationHp;
    public float MaxFormationHp; 
    public float CurrentFormationStamina;
    public float MaxFormationStamina;

    public float formationSpacingx;
    public float formationSpacingz;  
    public float formationSpacingxMulti;
    public float formationSpacingzMulti;

    public bool isFormationRanged;
    public float formationRangedRange;
    public float formationMeleeRange;

    public GameObject[] StoredFormations = new GameObject[3];
    public GameObject[] EdgeOfMapLocations = new GameObject[3];

    private ArrayList Orders = new ArrayList();

    public GameObject UnitCard;

    private List<GameObject> tempar = new List<GameObject>();



    void Start()
    {
        Flag = this.transform.GetChild(0).gameObject;
        Morale = 100;
        goingToTemp = Instantiate(goingTo);
        goingToTemp.SetActive(false);
        EdgeOfMapLocations = GameObject.FindGameObjectsWithTag("RetreatPos");
        Player = GameObject.FindGameObjectWithTag("Player");

        FormationShape = Instantiate(StoredFormations[0]);

        GetForward();
        FormationShape.transform.position = this.gameObject.transform.position;
        FormationShape.transform.parent = this.gameObject.transform;
        for (int i = 0; i < UnitSize; i++)
        {

            UnitPawn = Instantiate(UnitPawn);
            UnitPawn.transform.position = this.gameObject.transform.position;
            UnitManaged.Add(UnitPawn);
        }
        MaxFormationHp = GetUnitMaxHp();
        MaxFormationStamina = GetUnitMaxStamina();

        GetUnitRanged();
        StartCoroutine(UpdateMorale());
        UpdateShape(new Vector3(1,0,0));
    }



    public float GetUnitMaxHp()
    {
        float hP = 0;
        for (int i = 0; i < UnitManaged.Count; i++)
        {
            hP += UnitManaged[i].GetComponent<PawnController>().hP;
        }
        return hP;
    }   
    public void GetUnitRanged()
    {
        if (UnitManaged.Count != 0)
        {
            isFormationRanged = UnitManaged[0].GetComponent<PawnController>().isRanged;
            formationRangedRange = UnitManaged[0].GetComponent<PawnController>().pawnRangedRange;
            formationMeleeRange = UnitManaged[0].GetComponent<PawnController>().pawnMeleeRange;
        }
    }
    public float GetUnitMaxStamina()
    {

        float stam = 0;
        for (int i = 0; i < UnitManaged.Count; i++)
        {
            stam += UnitManaged[i].GetComponent<PawnController>().stamina;
        }    
        return stam;
    }

    public float GetUnitHp()
    {
        float hP = 0;
        for (int i = 0; i < UnitManaged.Count; i++)
        {
            hP += UnitManaged[i].GetComponent<PawnController>().hP;
        }

        hP = hP / MaxFormationHp;
        return hP;
    }
    public float GetUnitStamina()
    {

        float stam = 0;
        for (int i = 0; i < UnitManaged.Count; i++)
        {
            stam += UnitManaged[i].GetComponent<PawnController>().stamina;
        }
        stam = stam  / MaxFormationStamina;
        return stam;
    }


    // Update is called once per frame
    void Update()
    {

        if (UnitManaged.Count != 0)
        {
            Flag.transform.position = GetAveragePos();
            MoveToPos();
            MakeLook();
            CheckActive();
            CheckOrdersComplete();
            GetUnitHp();
            UpdateShapeLength();
            GetUnitRanged();
            UpdateMorale();
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }


    private IEnumerator UpdateMorale()
    {
        yield return new WaitForSeconds(0.5f);
        int layerMask = 1 << 9;
        if (CompareTag("Formation"))
        {
            layerMask = 1 << 7;

        }
        else if (CompareTag("EnemyFormation"))
        {
            layerMask = 1 << 3;
        }
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.right, out hit, 100000, layerMask))
        {
            Morale -= 0.7f;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100000, layerMask))
        {
            Morale -= 0.7f;
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 100000, layerMask))
        {
            Morale -= 0.7f;
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 100000, layerMask))
        {
            Morale -= 0.7f;
        }
        if (true)
        {

        }
        if (Morale < 100 && isBroken == false)
        {
            Morale += 1;
        }
        if (Morale < 50)
        {
            //wavering
        }
        if (Morale < 25)
        {
            isBroken = true;
            Retreat();
        }
    

    }
    public void Retreat()
    {
        GameObject closest = null;
        for (int i = 0; i < UnitManaged.Count; i++)
        {
            UnitManaged[i].GetComponent<PawnController>().Disengage();
        }
        for (int i = 0; i < EdgeOfMapLocations.Length; i++)
        {
            if (Vector3.Distance(transform.position,EdgeOfMapLocations[i].transform.position)< Vector3.Distance(transform.position, closest.transform.position))
            {
                closest = EdgeOfMapLocations[i];
            }
        }
        transform.position = closest.transform.position;
      
    }





    public void MakeLook()
    {
        for (int i = 0; i < UnitManaged.Count; i++)
        {
            UnitManaged[i].GetComponent<PawnController>().LookAt(Forward.transform.position);
        }

    }


    private void UpdateShapeLength()
    {
        FormationShape.transform.localScale = new Vector3(formationSpacingx,1,(formationSpacingz * unitDepth)/7);
    }


    public bool CheckOrdersComplete()
    {
        iDone = 0;

        for (int i = 0; i < UnitManaged.Count; i++)
        {
            if (UnitManaged[i].GetComponent<PawnController>().isOrderComplete == true)
            {
                iDone++;
            }               
        }
        if (iDone >= UnitManaged.Count / 1.2)
        {
            return true;    
            
        }
        else
        {
            return false;
        }

    }


    public void GetForward()
    {
        Forward = FormationShape.transform.GetChild(0).gameObject;
    }

    public void UpdateShape(Vector3 Order)
    {
        if (Order.x == 1)
        {
            currentShape = 1;
            Destroy(FormationShape);
            FormationShape = Instantiate(StoredFormations[0]);
            FormationShape.transform.position = this.gameObject.transform.position;
            FormationShape.transform.parent = this.gameObject.transform;
            FormationShape.transform.LookAt(Forward.transform.position);
            formationSpacingx = 1f * formationSpacingxMulti; 
            formationSpacingz = 1f * formationSpacingzMulti;
}
        else if (Order.x == 2)
        {
            currentShape = 2;
            Destroy(FormationShape);
            FormationShape = Instantiate(StoredFormations[1]);
            FormationShape.transform.position = this.gameObject.transform.position;
            FormationShape.transform.parent = this.gameObject.transform;
            FormationShape.transform.LookAt(Forward.transform.position);
            formationSpacingx = 1.2f * formationSpacingxMulti;
            formationSpacingz = 1.2f * formationSpacingzMulti;
        }
        else if (Order.x == 3)
        {
            currentShape = 3;
            Destroy(FormationShape);
            FormationShape  = Instantiate(StoredFormations[2]);
            FormationShape.transform.position = this.gameObject.transform.position;
            FormationShape.transform.parent = this.gameObject.transform;
            FormationShape.transform.LookAt(Forward.transform.position);
            formationSpacingx = 2f * formationSpacingxMulti;
            formationSpacingz = 2f * formationSpacingzMulti;
        }
        GetForward();
        Flag = FormationShape.transform.GetChild(1).gameObject;
    }



    public void OrderClear()
    {
        Orders.Clear();
    }


    private IEnumerator looper()
    {
        yield return new WaitForSeconds(0.1f);
        if (CheckOrdersComplete())
        {
            goingToTemp.SetActive(false);
        }
        if (Orders.Count != 0)
        {
            DoOrders();
        }
    }

    public Vector3 GetAveragePos()
    {
        Vector3 meanVector = Vector3.zero;

        foreach (GameObject pos in UnitManaged)
        {
            meanVector += pos.transform.position;
        }

        meanVector = meanVector / UnitManaged.Count;

        return (meanVector);
    }

    public void DoOrders()
    {
        if (Orders.Count == 0)
        {
            StopAllCoroutines();

        }
        if (CheckOrdersComplete() && Orders.Count > 0 && isBroken == false)
        {
            if (Orders[0].ToString() == "MoveTo")
            {
                goingToTemp.SetActive(true);

                for (int i = 0; i < UnitManaged.Count; i++)
                {
                    UnitManaged[i].GetComponent<PawnController>().Moving();
                }
                this.gameObject.transform.rotation = Player.transform.rotation;
                this.gameObject.transform.position = (Vector3)Orders[1];
                goingToTemp.transform.position = this.gameObject.transform.position;
                goingToTemp.transform.rotation = this.gameObject.transform.rotation;
                FormationShape.transform.rotation = new Quaternion(0, 0, 0, 0);
                Orders.RemoveAt(0);
                Orders.RemoveAt(0);

            }


            else if (Orders[0].ToString() == "Attack")
            {



                GameObject temp = (GameObject)Orders[1];
                this.gameObject.transform.LookAt(temp.transform.position);
                if (!isFormationRanged)
                {
                    Vector3 newPos = Vector3.MoveTowards(temp.transform.position, this.transform.position, ((unitDepth - 3)*formationSpacingz)+formationMeleeRange);
                    
                    this.gameObject.transform.position = newPos;


                    for (int i = 0; i < UnitManaged.Count; i++)
                    {
                        UnitManaged[i].GetComponent<PawnController>().Charge();
                    }
                }
                else
                {
                    if (Vector3.Distance(FormationShape.transform.position,temp.transform.position) < formationRangedRange)
                    {
                        for (int i = 0; i < UnitManaged.Count; i++)
                        {
                            UnitManaged[i].GetComponent<PawnController>().RangedAttack(temp);

                        }
                    }
                    else
                    {
                        Vector3 newPos = Vector3.MoveTowards(temp.transform.position,this.transform.position,formationRangedRange - unitDepth);
                        transform.position = newPos;
                        Orders.Add(Orders[0]);
                        Orders.Add(Orders[1]);

                    }
                }
                Orders.RemoveAt(0);
                Orders.RemoveAt(0);
                FormationShape.transform.rotation = new Quaternion(0, 0, 0, 0);    
            }

            else if (Orders[0].ToString() == "Advance")
            {
                for (int i = 0; i < UnitManaged.Count; i++)
                {
                    UnitManaged[i].GetComponent<PawnController>().Moving();
                }
                transform.Translate(this.gameObject.transform.right + (Vector3)Orders[1]);
                Orders.RemoveAt(0);
                Orders.RemoveAt(0);
            } 
            else if (Orders[0].ToString() == "Run")
            {

                for (int i = 0; i < UnitManaged.Count; i++)
                {
                    UnitManaged[i].GetComponent<PawnController>().ToggleRun();
                }
                Orders.RemoveAt(0);
                Orders.RemoveAt(0);
            }
            else if (Orders[0].ToString() == "ChangeSpeed")
            {

                transform.Translate(this.gameObject.transform.right + (Vector3)Orders[1]);
                Orders.RemoveAt(0);
                Orders.RemoveAt(0);
            }

            else if (Orders[0].ToString() == "Rotate")
            {

                this.transform.Rotate((Vector3)Orders[1]);
                Orders.RemoveAt(0);
                Orders.RemoveAt(0);
            }  
            else if (Orders[0].ToString() == "LookAt")
            {

                this.transform.LookAt((Vector3)Orders[1]);
                Orders.RemoveAt(0);
                Orders.RemoveAt(0);
            }

            else if (Orders[0].ToString() == "FormationChange")
            {
                UpdateShape((Vector3)Orders[1]);
                Orders.RemoveAt(0);
                Orders.RemoveAt(0);
            }
        }
        StartCoroutine("looper");

    }

    public void ReceiveOrder(Vector3 Order, string orderType)
    {
        if (isBroken == false)
        {
            Orders.Add(orderType);
            Orders.Add(Order);
        }

    }  
    public void ReceiveAttackOrder(GameObject Order, string orderType)
    {
        if (isBroken == false)
        {
            Orders.Add(orderType);
            Orders.Add(Order);
        }
    }


    public void MoveToPos()
    {
        tempar.Clear();
        Vector3[] tempVerxArray =  FormationShape.GetComponent<MeshFilter>().mesh.vertices;
        Vector3[] tempWorldPos = new Vector3[tempVerxArray.Length];
        int countInFormation = 0;

        for (int i = 0; i < tempVerxArray.Length; i++)
        {
            tempWorldPos[i] = FormationShape.transform.TransformPoint(tempVerxArray[i]);
        }


        Vector3 startx = tempWorldPos[2];
        Vector3 endx = tempWorldPos[1];
        float o = formationSpacingx ;

        Debug.DrawLine(startx, endx);
        Debug.DrawLine(tempWorldPos[5], tempWorldPos[6]);
        Vector3 nextPos = startx;
        unitDepth = 1;
        unitWidth = 0;

        for (int i = 0; i < UnitManaged.Count; i++)
        {
            if (UnitManaged[i].GetComponent<PawnController>().inFormation)
            {
                tempar.Add(UnitManaged[i]);
                countInFormation += 1;
            }
        }
        for (int i = 0; i < countInFormation; i++)
        {
            float oldDistance = Mathf.Infinity;
            int location = 0;
            for (int x = 0; x < tempar.Count; x++)
            {

                float dist = Vector3.Distance(tempar[x].transform.position, nextPos);
                if (dist < oldDistance)
                {
                    GameObject closetsObject = tempar[x];
                    location = x;
                    oldDistance = dist;
                }
            }
            tempar[location].GetComponent<PawnController>().NewOrder(nextPos);
            tempar[location].GetComponent<PawnController>().LookAt(Forward.transform.position);
            tempar.RemoveAt(location);
            nextPos = Vector3.MoveTowards(startx, endx, o);
            if ( Vector3.Distance(nextPos, endx) < 0.2)
            {
                unitDepth += 1;
                if (currentShape != 2)
                {
                    o = 0;
                    startx = Vector3.MoveTowards(startx, tempWorldPos[6], formationSpacingz);
                    endx = Vector3.MoveTowards(endx, tempWorldPos[7], formationSpacingz);
                }
                else
                {
                    o = 0;
                    startx = Vector3.MoveTowards(startx, tempWorldPos[6], formationSpacingz);
                    endx = Vector3.MoveTowards(endx, tempWorldPos[5], formationSpacingz);
                }
            }
            else
            {
                o += formationSpacingx;
            }
        }
    }

    public void CheckActive()
    {
        for (int i = 0; i < UnitManaged.Count; i++)
        {
            if (UnitManaged[i].gameObject.tag == "Dead")
            {
                UnitManaged.RemoveAt(i);
            }
        }
        if (UnitManaged.Count <= 0)
        {
            //remove colliders
        }
    }
}
