using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestReward", menuName = "ScriptableObjects/QuestRW", order = 2)]

public class QuestReward : ScriptableObject
{

    public int moneyReward;
    public GameObject[] itemRewards;
}