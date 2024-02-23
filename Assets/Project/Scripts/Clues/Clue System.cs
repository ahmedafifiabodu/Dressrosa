using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue_System : MonoBehaviour
{
    public static Clue_System instance;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private List<Clues> Quest1_objectives , Quest2_objectives , Quest3_objectives;
    public int questIndex;
    public int objectiveIndex;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        questIndex = questManager.GetActiveQuestIndex();
        if (questIndex != -1)
        {
            Quest activeQuest = questManager.quests[questIndex];
            objectiveIndex = activeQuest.GetActiveObjectiveIndex();
            if(questIndex == 0)
            {
                ActiveObjectives(Quest1_objectives);
            }
            else if(questIndex == 1)
            {
                ActiveObjectives(Quest2_objectives);
            }
            else if (questIndex == 2)
            {
                ActiveObjectives(Quest3_objectives);
            }
        }
    }

    private void ActiveObjectives(List<Clues> obj)
    {
        if (TimeTravel_System.instance.canTravel && TimeTravel_System.instance.effectActivated)
        {
            for (int j = 0; j < obj[objectiveIndex].clues.Count; j++)
            {
                obj[objectiveIndex].clues[j].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int j = 0; j < obj[objectiveIndex].clues.Count; j++)
            {
                obj[objectiveIndex].clues[j].gameObject.SetActive(false);
            }
        }
    }
}
