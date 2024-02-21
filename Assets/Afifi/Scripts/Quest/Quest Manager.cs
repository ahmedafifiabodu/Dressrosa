using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [Header("Quests")]
    [SerializeField] internal List<Quest> quests = new();

    [Header("UI")]
    [SerializeField] private GameObject _questCanvas;

    [SerializeField] private Image _questIcon;
    [SerializeField] private TextMeshProUGUI _questName;
    [SerializeField] private TextMeshProUGUI _objectiveName;
    [SerializeField] private TextMeshProUGUI _objectiveDescription;

    internal int lastCompletedQuestIndex = -1;

    internal void ResetQuests()
    {
        foreach (var quest in quests)
        {
            quest.IsActive = false;

            foreach (var objective in quest.objectives)
                objective.isCompleted = false;
        }
    }

    private void Start() => ResetQuests();

    internal int GetActiveQuestIndex()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].IsActive && !quests[i].IsCompleted)
                return i;
        }
        return -1;
    }

    internal void SetObjectiveCompletion(int questIndex, int objectiveIndex, bool isCompleted)
    {
        if (questIndex < 0 || questIndex >= quests.Count)
            return;

        Quest quest = quests[questIndex];

        if (objectiveIndex < 0 || objectiveIndex >= quest.objectives.Count)
            return;

        quest.objectives[objectiveIndex].isCompleted = isCompleted;

        if (quest.IsCompleted)
        {
            lastCompletedQuestIndex = questIndex;
        }

        /*
                 // Check if the quest is completed
        if (quest.IsCompleted)
        {
            // Update the last completed quest index
            lastCompletedQuestIndex = questIndex;

            // Update the UI
            UpdateUI();
        }*/
    }

    internal void UpdateUI()
    {
        if (lastCompletedQuestIndex != -1)
        {
            // If there is a completed quest
            Quest lastCompletedQuest = quests[lastCompletedQuestIndex];
            _questIcon.sprite = null;
            _questName.text = $"The {lastCompletedQuest.questName} quest has been completed.";
            _objectiveName.text = "";
            _objectiveDescription.text = "";
        }
        else
        {
            int activeQuestIndex = GetActiveQuestIndex();
            if (activeQuestIndex != -1)
            {
                // If there is an active quest
                Quest activeQuest = quests[activeQuestIndex];
                _questIcon.sprite = activeQuest.icon;
                _questName.text = activeQuest.questName;
                _objectiveName.text = activeQuest.objectives[activeQuest.GetActiveObjectiveIndex()].objectiveName;
                _objectiveDescription.text = activeQuest.objectives[activeQuest.GetActiveObjectiveIndex()].description;
            }
            else
            {
                // If there is no active quest
                _questIcon.sprite = null;
                _questName.text = "No Active Quest";
                _objectiveName.text = "";
                _objectiveDescription.text = "";
            }
        }
    }

    internal void SetActiveForQuestPanel(bool _setActive) => _questCanvas.SetActive(_setActive);

    internal void StartQuest(int questIndex)
    {
        if (questIndex < 0 || questIndex >= quests.Count)
            return;

        // Reset the quest
        foreach (var objective in quests[questIndex].objectives)
            objective.isCompleted = false;

        // Set the quest as active
        quests[questIndex].IsActive = true;

        lastCompletedQuestIndex = -1;
    }

    private void Update() => UpdateUI();
}