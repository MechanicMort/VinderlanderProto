using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPeople : MonoBehaviour
{

    public GameObject content;
    public bool done;
    public int  place;
    // Start is called before the first frame update

    private void OnEnable()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        place = GameObject.FindGameObjectsWithTag("Person").Length - 1;
        content = GameObject.FindGameObjectWithTag("Content");
        StartCoroutine(AddChildren());
    }

    private IEnumerator AddChildren()
    {

        GameObject UnitCardTemp = Instantiate(GameObject.FindGameObjectsWithTag("Person")[place].GetComponent<PawnController>().personCard, content.transform);
        UnitCardTemp.GetComponent<PersonCard>().myPerson = GameObject.FindGameObjectsWithTag("Person")[place].GetComponent<PawnController>();
        UnitCardTemp.GetComponent<RectTransform>().sizeDelta = new Vector2(2000,200);
        place -= 1;
       
        yield return new WaitForEndOfFrame();
        if (place > -1)
        {
            StartCoroutine(AddChildren());
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
