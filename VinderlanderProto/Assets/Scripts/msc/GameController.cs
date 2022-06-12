using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{

    public TextMeshProUGUI timeDisplay;
    public TextMeshProUGUI moneyDisplay;



    public int money = 10;
    public float timeOfDay;
    public float timeStep = 0.1f;


    private IEnumerator MoveTime()
    {
        yield return new WaitForSeconds(0.1f);
        timeOfDay += timeStep;
        if (timeOfDay >= 24)
        {
            timeOfDay = 0;
        }
        StartCoroutine("MoveTime");
    }

    private void Start()
    {
        StartCoroutine("MoveTime");
    }
    private void Update()
    {
        moneyDisplay.text = money.ToString();
        timeDisplay.text = timeOfDay.ToString();
    }

}
