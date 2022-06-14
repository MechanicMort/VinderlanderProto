using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    private PlayerInventory playerInventory;
    public TextMeshProUGUI questObjective;
    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;
    public Quest[] quests;
    public Quest activeQuest;
    private int quest = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        quests = new Quest[GameObject.FindGameObjectsWithTag("QuestHolder").Length];
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("QuestHolder").Length; i++)
        {
            quests[i] = GameObject.FindGameObjectsWithTag("QuestHolder")[i].GetComponent<QuestHolder>().Quest;
            if (quests[i].orderIn == 0)
            {
                activeQuest = quests[i];
            }
        }
   
    }

    // Update is called once per frame
    void Update()
    {

        questObjective.text = activeQuest.currentObjective;
        questName.text = activeQuest.questName;
        questDescription.text = activeQuest.questDescription;
        if (quest != activeQuest.orderIn)
        {
            Instantiate(activeQuest.Reward);
            for (int i = 0; i < quests.Length; i++)
            {
                if (quests[i].orderIn == activeQuest.orderIn + 1)
                {
                    activeQuest = quests[i];
                    break;
                }
            }
        }
        switch (activeQuest.orderIn)
        {
            case 0:
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().velocity.magnitude >= 4)
                {
                    quest += 1;
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    quest += 1;
                }
                break;
        }
            
    }
}
