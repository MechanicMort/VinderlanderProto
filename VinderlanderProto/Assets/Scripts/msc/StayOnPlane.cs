using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnPlane : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;

        if (Physics.Raycast(transform.position,transform.up , out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {

            transform.position = hit.point;    
        }   
        else if (Physics.Raycast(transform.position,-transform.up , out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            transform.position = hit.point;   
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 1, 0), 0.01f);
        }
    }
}
