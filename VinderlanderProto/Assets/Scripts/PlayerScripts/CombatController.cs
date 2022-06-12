using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatController : MonoBehaviour
{
    public Image left;
    public Image right;
    public Image up;
    public Image down;
    public Image centre;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //will need an enter combat mode and tactical mode thingy

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if ((Input.GetAxis("Mouse Y")) * (Input.GetAxis("Mouse Y")) < (((Input.GetAxis("Mouse X")) * (Input.GetAxis("Mouse X")))))
            {
                switch (Input.GetAxis("Mouse X"))
                {
                    case > 0.25f:
                        right.enabled = true;
                        left.enabled = false;
                        up.enabled = false;
                        down.enabled = false;
                        centre.enabled = false;
                        break;
                    case < -0.25f:
                        right.enabled = false;
                        left.enabled = true;
                        up.enabled = false;
                        down.enabled = false;
                        centre.enabled = false;
                        break;
                }
            }
            else
            {
                switch (Input.GetAxis("Mouse Y"))
                {
                    case > 0.25f:
                        right.enabled = false;
                        left.enabled = false;
                        up.enabled = true;
                        down.enabled = false;
                        centre.enabled = false;
                        break;
                    case < -0.25f:
                        right.enabled = false;
                        left.enabled = false;
                        up.enabled = false;
                        down.enabled = true;
                        centre.enabled = false;
                        break;
                }
            }
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            right.enabled = false;
            left.enabled = false;
            up.enabled = false;
            down.enabled = false;
            centre.enabled = false;
            centre.enabled = true;

        }
       
    
  
    }
}
