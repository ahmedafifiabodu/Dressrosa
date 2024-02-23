using UnityEngine;

public class SetObjective : MonoBehaviour
{
    public QuestManager questManager;
    public bool isCompleted;
    public int questIndex;
    public int objectiveIndex;

    public void SetObjectiveCompletion()
    {
        questIndex = questManager.GetActiveQuestIndex();
        if (questIndex != -1)
        {
            Quest activeQuest = questManager.quests[questIndex];
            objectiveIndex = activeQuest.GetActiveObjectiveIndex();

            // activeQuest.objectives[objectiveIndex].setObjectsToTrue();
            if (objectiveIndex != -1)
            {
                questManager.SetObjectiveCompletion(questIndex, objectiveIndex, isCompleted);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.PLAYERTAG))
        {
            isCompleted = true;

            if (objectiveIndex == 1)
                objectiveIndex = 0;

            SetObjectiveCompletion();
        }
    }

    private void Update()
    {
        questIndex = questManager.GetActiveQuestIndex();

        if (questIndex != -1)
            objectiveIndex = questManager.quests[questIndex].GetActiveObjectiveIndex();
    }
}