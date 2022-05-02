using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float timeOfDay;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MoveTime");
        //DontDestroyOnLoad(transform.gameObject);
    }

    private IEnumerator MoveTime()
    {
        yield return new WaitForSeconds(0.1f);
        timeOfDay += 0.1f;
        if (timeOfDay >= 24)
        {
            timeOfDay = 0;
        }
        StartCoroutine("MoveTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
