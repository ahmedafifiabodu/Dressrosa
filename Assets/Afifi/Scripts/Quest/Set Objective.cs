using UnityEngine;

public class SetObjective : MonoBehaviour
{
    public QuestManager questManager;
    public bool isCompleted;

    public void SetObjectiveCompletion()
    {
        int questIndex = questManager.GetActiveQuestIndex();
        if (questIndex != -1)
        {
            Quest activeQuest = questManager.quests[questIndex];
            int objectiveIndex = activeQuest.GetActiveObjectiveIndex();
            if (objectiveIndex != -1)
            {
                questManager.SetObjectiveCompletion(questIndex, objectiveIndex, isCompleted);
                Debug.Log("Objective completed");
            }
            else
            {
                Debug.LogError("No active objective");
            }
        }
        else
        {
            Debug.LogError("No active quest");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCompleted = true;
            SetObjectiveCompletion();
            Debug.Log("Trigger completed");
        }
    }
}