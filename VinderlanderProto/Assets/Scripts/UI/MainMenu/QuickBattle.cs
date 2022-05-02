using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickBattle : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartQuickBattle()
    {
        SceneManager.LoadScene(1);
    }
}
