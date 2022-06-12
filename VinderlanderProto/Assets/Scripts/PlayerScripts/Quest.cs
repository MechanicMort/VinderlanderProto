using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/QuestSO", order = 1)]

public class Quest : ScriptableObject
{

        public string questName;
        public string currentObjective;
        public string questDescription;
        public int questSteps;
        public GameObject Reward;
        public QuestSystem questSystem;
        public int orderIn;   
}
