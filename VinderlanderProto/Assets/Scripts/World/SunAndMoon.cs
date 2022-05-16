using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SunAndMoon : MonoBehaviour
{
    public GameObject lightSource;
    public bool sunOrMoon;
    private GameController gameController;
    private Light directionalLight;
    // Start is called before the first frame update
    void Start()
    {
        lightSource = this.transform.gameObject;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        directionalLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

         lightSource.transform.Rotate(new Vector3(0.05f, 0, 0)); 

    }
}
