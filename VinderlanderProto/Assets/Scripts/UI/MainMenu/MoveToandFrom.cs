using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToandFrom : MonoBehaviour
{

    public GameObject pos1;
    public GameObject Cam;
    public GameObject pos2;
    public GameObject neededPos;
    public GameObject BattlePanel;
    public GameObject MainPanel;
    public float moveSpeed = 0.01f;


    // Start is called before the first frame update


    public void Update()
    {

        Cam.transform.position = Vector3.Lerp(Cam.transform.position, neededPos.transform.position, moveSpeed);
    }

    public void MainToBattle()
    {

        BattlePanel.SetActive(true);
        MainPanel.SetActive(false);
        neededPos = pos2;
        Cam.transform.rotation = pos2.transform.rotation;
    }

    public void BattleToMain()
    {
        MainPanel.SetActive(true);
        BattlePanel.SetActive(false);
        neededPos = pos1;
        Cam.transform.rotation = pos1.transform.rotation;
    }

}
