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
            if (!quests[i].IsCompleted)
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
            Debug.LogError("No active quest");
        }
    }

    internal void SetActiveForQuestPanel(bool _setActive) => _questCanvas.SetActive(_setActive);

    private void Update() => UpdateUI();
}