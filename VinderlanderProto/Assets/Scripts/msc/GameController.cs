using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{

    public GameObject woodDisplay;
    public GameObject stoneDisplay;
    public GameObject metalDisplay;

    public float timeOfDay;
    public float timeStep = 0.1f;

    public Dictionary<string, float> ResourcesStored = new Dictionary<string, float>();

    // Start is called before the first frame update
    void Start()
    {
        ResourcesStored.Add("Wood", 0);
        ResourcesStored.Add("Stone", 0);
        ResourcesStored.Add("Metal", 0);
        StartCoroutine("MoveTime");
        //DontDestroyOnLoad(transform.gameObject);
    }

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

    private void DisplayResources()
    {
        woodDisplay.GetComponent<TextMeshProUGUI>().text = "Wood = " + ResourcesStored["Wood"];
        stoneDisplay.GetComponent<TextMeshProUGUI>().text = "Stone =" + ResourcesStored["Stone"];
        metalDisplay.GetComponent<TextMeshProUGUI>().text = "Metal =" + ResourcesStored["Metal"];
    }

    private void GetResources()
    {
        //loop through resource depos and check contents
    }

    // Update is called once per frame
    void Update()
    {
        DisplayResources();
    }
}
