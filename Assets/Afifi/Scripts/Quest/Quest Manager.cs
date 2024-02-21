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

    internal void ResetQuests()
    {
        foreach (var quest in quests)
        {
            quest.isStarted = false;
            foreach (var objective in quest.objectives)
            {
                objective.isCompleted = false;
            }
        }
    }

    private void Start() => ResetQuests();

    internal int GetActiveQuestIndex()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            // Check if the quest has been started and is not completed
            if (quests[i].isStarted && !quests[i].IsCompleted)
                return i;
        }
        return -1;
    }

    internal void SetObjectiveCompletion(int questIndex, int objectiveIndex, bool isCompleted)
    {
        if (questIndex < 0 || questIndex >= quests.Count)
        {
            Debug.LogError("Invalid quest index");
            return;
        }

        Quest quest = quests[questIndex];

        if (objectiveIndex < 0 || objectiveIndex >= quest.objectives.Count)
        {
            Debug.LogError("Invalid objective index");
            return;
        }

        quest.objectives[objectiveIndex].isCompleted = isCompleted;
    }

    private void UpdateUI()
    {
        int activeQuestIndex = GetActiveQuestIndex();

        if (activeQuestIndex != -1)
        {
            Quest activeQuest = quests[activeQuestIndex];

            _questIcon.sprite = activeQuest.icon;
            _questName.text = activeQuest.questName;

            _objectiveName.text = activeQuest.objectives[activeQuest.GetActiveObjectiveIndex()].objectiveName;
            _objectiveDescription.text = activeQuest.objectives[activeQuest.GetActiveObjectiveIndex()].description;
        }
        else
        {
            // Clear the UI fields if there is no active quest
            _questIcon.sprite = null;
            _questName.text = "";
            _objectiveName.text = "";
            _objectiveDescription.text = "";
        }
    }

    internal void SetActiveForQuestPanel(bool _setActive) => _questCanvas.SetActive(_setActive);

    internal void StartQuest(int questIndex)
    {
        if (questIndex < 0 || questIndex >= quests.Count)
        {
            Debug.LogError("Invalid quest index");
            return;
        }

        // Mark the quest as started
        quests[questIndex].isStarted = true;

        // Reset the quest
        foreach (var objective in quests[questIndex].objectives)
        {
            objective.isCompleted = false;
        }
    }

    private void Update() => UpdateUI();
}