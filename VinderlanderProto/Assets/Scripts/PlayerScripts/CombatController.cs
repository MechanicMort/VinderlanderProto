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
    public GameObject swordPosUp;
    public GameObject swordPosDown;
    public GameObject swordPosLeft;
    public GameObject swordPosRight;
    public GameObject swordPosCentre;
    public GameObject shieldPosDefault;
    public GameObject shieldPosBlock;
    public GameObject swordAttackPos;
    public GameObject sword;
    public PlayerSword swordController;
    public GameObject shield;
    private string attackType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {


    }
    void Update()
    {

        //will need an enter combat mode and tactical mode thingy
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            attackType = "attack";
            swordController.isAttacking = true;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            attackType = "block";
            right.enabled = true;
            left.enabled = true;
            up.enabled = true;
            down.enabled = true;
            centre.enabled = true;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            if ((Input.GetAxis("Mouse Y")) * (Input.GetAxis("Mouse Y")) < (((Input.GetAxis("Mouse X")) * (Input.GetAxis("Mouse X")))))
            {
                switch (Input.GetAxis("Mouse X"))
                {
                    case > 0.25f:
                        attackType = "right";
                        right.enabled = true;
                        left.enabled = false;
                        up.enabled = false;
                        down.enabled = false;
                        centre.enabled = false;
                        break;
                    case < -0.25f:
                        attackType = "left";
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
                        attackType = "up";
                        right.enabled = false;
                        left.enabled = false;
                        up.enabled = true;
                        down.enabled = false;
                        centre.enabled = false;
                        break;
                    case < -0.25f:
                        attackType = "down";
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
            attackType = "centre";
            right.enabled = false;
            left.enabled = false;
            up.enabled = false;
            down.enabled = false;
            centre.enabled = false;
            centre.enabled = true;

        }
        else if (attackType == "attack" && Vector3.Distance(sword.transform.position,swordAttackPos.transform.position )<= 0.05f)
        {
            swordController.isAttacking = false;
            attackType = "none";
        }
        
          
        


        switch (attackType)
        {
            case "none":

                sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosCentre.transform.position, 0.1f);
                sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosCentre.transform.rotation, 0.1f);
                //sword.transform.SetParent(swordPosRight.transform, false);
                // shield.transform.SetParent(shieldPosDefault.transform, false);
                shield.transform.position = Vector3.Lerp(shield.transform.position, shieldPosDefault.transform.position, 0.1f);
                shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldPosDefault.transform.rotation, 0.1f);
                break;
            case "right":
                sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosRight.transform.position,0.1f);
                sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosRight.transform.rotation, 0.1f);
                //sword.transform.SetParent(swordPosRight.transform, false);
               // shield.transform.SetParent(shieldPosDefault.transform, false);

                shield.transform.position = Vector3.Lerp(shield.transform.position, shieldPosDefault.transform.position, 0.1f);
                shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldPosDefault.transform.rotation, 0.1f);
                break;
            case "left":
                sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosLeft.transform.position, 0.1f);
                sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosLeft.transform.rotation, 0.1f);
                //  sword.transform.SetParent(swordPosLeft.transform, false);
              //  shield.transform.SetParent(shieldPosDefault.transform, false);

                shield.transform.position = Vector3.Lerp(shield.transform.position, shieldPosDefault.transform.position, 0.1f);
                shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldPosDefault.transform.rotation, 0.1f);
                break;
            case "up":
                sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosUp.transform.position, 0.1f);
                sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosUp.transform.rotation, 0.1f);
                //   sword.transform.SetParent(swordPosUp.transform,false);
               // shield.transform.SetParent(shieldPosDefault.transform, false);

                shield.transform.position = Vector3.Lerp(shield.transform.position, shieldPosDefault.transform.position, 0.1f);
                shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldPosDefault.transform.rotation, 0.1f);
                break;
            case "down":
                sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosDown.transform.position, 0.1f);
                sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosDown.transform.rotation, 0.1f);
                // sword.transform.SetParent(swordPosDown.transform, false);
               // shield.transform.SetParent(shieldPosDefault.transform, false);

                shield.transform.position = Vector3.Lerp(shield.transform.position, shieldPosDefault.transform.position, 0.1f);
                shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldPosDefault.transform.rotation, 0.1f);
                break;
            case "centre":
                sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosCentre.transform.position, 0.1f);
                sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosCentre.transform.rotation, 0.1f);
                //  sword.transform.SetParent(swordPosCentre.transform, false);
               // shield.transform.SetParent(shieldPosDefault.transform, false);
               
                shield.transform.position = Vector3.Lerp(shield.transform.position, shieldPosDefault.transform.position, 0.1f);
                shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldPosDefault.transform.rotation, 0.1f);
                break;
            case "block":
                sword.transform.position = Vector3.Lerp(sword.transform.position, swordPosCentre.transform.position, 0.1f);
                sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordPosCentre.transform.rotation, 0.1f);
               // sword.transform.SetParent(swordPosCentre.transform, false);
              //  shield.transform.SetParent(shieldPosBlock.transform,false);
                shield.transform.position = Vector3.Lerp(shield.transform.position, shieldPosBlock.transform.position, 0.1f);
                shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldPosBlock.transform.rotation, 0.1f);
                break;
            case "attack":
                sword.transform.position = Vector3.Lerp(sword.transform.position, swordAttackPos.transform.position, 0.2f);
                sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, swordAttackPos.transform.rotation, 0.4f);
                // sword.transform.SetParent(swordPosCentre.transform, false);
                //  shield.transform.SetParent(shieldPosBlock.transform,false);
                shield.transform.position = Vector3.Lerp(shield.transform.position, shieldPosDefault.transform.position, 0.1f);
                shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldPosDefault.transform.rotation, 0.1f);
                break;
        }
              
            
        
       
    
  
    }
}
