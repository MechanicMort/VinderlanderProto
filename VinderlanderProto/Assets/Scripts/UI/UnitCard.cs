using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCard : MonoBehaviour
{


    public Image UnitImage;
    public Image Hp;
    public Image Stam;  

    public float HpFloat;
    public float StamFloat;

    public GameObject FormationManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hp.fillAmount = FormationManager.GetComponent<FormationManager>().GetUnitHp();
        Stam.fillAmount = FormationManager.GetComponent<FormationManager>().GetUnitStamina();
    }
}
