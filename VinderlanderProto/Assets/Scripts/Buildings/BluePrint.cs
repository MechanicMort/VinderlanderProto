using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{
    private bool isFinished = false;
    public GameObject finishedBuilding;
    public Dictionary<string, float> materialsNeeded = new Dictionary<string, float>();
    // Start is called before the first frame update
    public float woodCost;
    public float stoneCost;
    public float metalCost;
    public float workCost;
    // Start is called before the first frame update
    void Start()
    {
        if (woodCost!= 0 )
        {
            materialsNeeded.Add("Wood", woodCost);
        }
        if (stoneCost != 0)
        {
            materialsNeeded.Add("Stone", stoneCost);
        }
        if (metalCost != 0)
        {
            materialsNeeded.Add("Metal", metalCost);
        }
        if (workCost != 0)
        {
            materialsNeeded.Add("Work", workCost);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //float fOut;
        //buildCost.TryGetValue("Wood", out fOut);
        //print("Wood :" + fOut);
        //buildCost.TryGetValue("Stone", out fOut);
        //print("Stone :" + fOut);
        //buildCost.TryGetValue("Metal", out fOut);
        //print("Metal :" + fOut);
        //buildCost.TryGetValue("Work", out fOut);
        //print("Work :" + fOut);
        if (CheckDone())
        {
            Instantiate(finishedBuilding,transform.position,transform.rotation,null);
            Destroy(this.gameObject);
        }
    }

    public void ApplyWork(float amount)
    {
        materialsNeeded["Work"] = materialsNeeded["Work"] - amount;
        if (materialsNeeded["Work"] <= 0)
        {
            materialsNeeded.Remove("Work");
        }
    }

    public void ApplyMats(string Mat, float amount) {
        if (materialsNeeded.ContainsKey(Mat))
        {
            materialsNeeded[Mat] = materialsNeeded[Mat] - amount;
            if (materialsNeeded[Mat] <= 0)
            {
                materialsNeeded.Remove(Mat);
            }
        }
        else
        {
            print("Material Not Found");
        }
   

    }
    private bool CheckDone()
    {
        if (materialsNeeded.Count == 0)
        {
            print(materialsNeeded.Count);
            return true;
        }
        return false;
    }
}
