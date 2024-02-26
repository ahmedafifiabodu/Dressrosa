using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] internal List<Quest> quests = new();

    [Header("Quest UI")]
    [SerializeField] private GameObject _questCanvas;

    [SerializeField] private Sprite _defultQuestIcon;
    [SerializeField] private Image _questIcon;
    [SerializeField] private TextMeshProUGUI _questName;
    [SerializeField] private TextMeshProUGUI _objectiveDescription;

    [Header("Objective UI")]
    [SerializeField] private GameObject _objectiveObject;

    [SerializeField] private TextMeshProUGUI _objectiveName;

    internal int lastCompletedQuestIndex = -1;

    private TimeTravelSystem _timeTravelSystem;
    private ClueSystem _clueSystem;

    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    internal void ResetQuests()
    {
        _timeTravelSystem = TimeTravelSystem.Instance;
        _clueSystem = ClueSystem.Instance;

        foreach (var quest in quests)
        {
            quest.IsActive = false;

            foreach (var objective in quest.objectives)
                objective.isCompleted = false;
        }

        CheckTheQuests();
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
            _timeTravelSystem.canTravel = false;
            lastCompletedQuestIndex = questIndex;

            CheckTheQuests();
        }
    }

    internal void UpdateUI()
    {
        int activeQuestIndex = GetActiveQuestIndex();
        _objectiveObject.SetActive(activeQuestIndex != -1);

        if (lastCompletedQuestIndex != -1)
        {
            // If there is a completed quest
            Quest lastCompletedQuest = quests[lastCompletedQuestIndex];
            _questIcon.sprite = lastCompletedQuest.icon;
            _questName.text = $"The {lastCompletedQuest.questName} quest has been completed.";
            _objectiveName.text = "";
            _objectiveDescription.text = "";
        }
        else
        {
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
                _questIcon.sprite = _defultQuestIcon;
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
        _timeTravelSystem.canTravel = true;
        lastCompletedQuestIndex = -1;

        // Activate the objectives for the quest
        _clueSystem.ActiveObjectives(questIndex);
    }

    private void CheckTheQuests()
    {
        int activeQuestIndex = GetActiveQuestIndex();

        // If there is no active quest
        if (activeQuestIndex == -1)
        {
            // Loop through the quests list
            for (int i = 0; i < quests.Count; i++)
            {
                Quest quest = quests[i];

                if (!quest.IsCompleted && quest.needsDialog)
                    break;

                // If the quest is inactive, not completed and doesn't need a dialog
                if (!quest.IsActive && !quest.IsCompleted && !quest.needsDialog)
                {
                    // Start the quest
                    StartQuest(i);
                    break; // Exit the loop as we've found a quest to start
                }
            }
        }
    }

    private void Update() => UpdateUI();
}