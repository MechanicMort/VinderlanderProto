using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public string storing;
    public float maxStored;
    public float stored;
    public bool isFull;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Person")
        {
            print("entered");
            if (other.GetComponent<PawnController>().resourceCarried != null)
            {
                print("Hasresource");
                if (other.GetComponent<PawnController>().resourceCarried.GetComponent<ResourceChunk>().resource.resource == storing && other.GetComponent<PawnController>().resourceCarried.GetComponent<ResourceChunk>().resource.amount <= maxStored-stored)
                {
                    print("Stored");
                    stored += other.GetComponent<PawnController>().resourceCarried.GetComponent<ResourceChunk>().resource.amount;
                    GameObject temp = other.GetComponent<PawnController>().resourceCarried;
                    other.GetComponent<PawnController>().resourceCarried = null;
                    Destroy(temp);
                    other.GetComponent<PawnController>().performingJob = false;
                    other.GetComponent<PawnController>().job = "";
                }
            }
        }
    }


}
