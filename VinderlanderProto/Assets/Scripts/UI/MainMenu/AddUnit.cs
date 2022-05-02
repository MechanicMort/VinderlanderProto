using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUnit : MonoBehaviour
{

    public GameObject Formation;
    public GameObject ArmyFormation;
    // Start is called before the first frame update
   


    public void addFormation()
    {
        ArmyFormation.GetComponent<ArmyManager>().alliedArmy[ArmyFormation.GetComponent<ArmyManager>().alliedArmy.Length] = Formation;
    }
}
