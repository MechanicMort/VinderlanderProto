using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{

    public GameObject timeDisplay;

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
        timeDisplay.GetComponent<TextMeshProUGUI>().text = timeOfDay.ToString();
        StartCoroutine("MoveTime");
    }

}
