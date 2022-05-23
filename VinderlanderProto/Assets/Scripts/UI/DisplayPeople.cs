using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPeople : MonoBehaviour
{

    public Transform content;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        done = false;
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Person").Length; i++)
        {
            GameObject UnitCardTemp;
            UnitCardTemp = Instantiate(GameObject.FindGameObjectsWithTag("Person")[i].GetComponent<PawnController>().personCard);
            UnitCardTemp.transform.SetParent(content, true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (done == false)
        {
            done = true;

        }
      
    }
}
