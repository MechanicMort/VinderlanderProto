using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PersonCard : MonoBehaviour
{
    public PawnController myPerson;
    public TextMeshProUGUI personName;
    public Image hunger;
    public float hungerAmount;
    public Image thirst;
    public float thirstAmount;
    public Image health;
    public float healthAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        personName.text = myPerson.pawnName;
        healthAmount = myPerson.hP / 100;
        hunger.fillAmount = hungerAmount;
        thirst.fillAmount = thirstAmount;
        health.fillAmount = healthAmount;

    }
}
