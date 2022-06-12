using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestRewardGiver : MonoBehaviour
{
    public QuestReward questReward;
    private GameController gameController;
    private GameObject player;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        GiveRewards();
        this.gameObject.SetActive(false);
    }
    public void GiveRewards()
    {
        for (int i = 0; i < questReward.itemRewards.Length; i++)
        {
            Instantiate(questReward.itemRewards[i],player.transform.position,player.transform.rotation,null);
        }
        if (questReward.moneyReward > 0)
        {
            gameController.money += questReward.moneyReward;
        }
    }
}



