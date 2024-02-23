using System.Collections.Generic;
using UnityEngine;

public class ClueSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] internal InputManager _inputManager;

    [SerializeField] private QuestManager questManager;

    [Header("Quest Objectives")]
    [SerializeField] private List<ClueGroup> questObjectives;

    public static ClueSystem Instance { get; private set; }

    private List<Clues> obj;
    private readonly HashSet<GameObject> completedClues = new();

    private TimeTravelSystem _timeTravelSystem;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() => _timeTravelSystem = TimeTravelSystem.Instance;

    internal void ActiveObjectives(int questIndex)
    {
        if (questObjectives == null || questIndex >= questObjectives.Count || questIndex < 0)
            return;

        obj = questObjectives[questIndex].clues;

        // Check if the quest is completed
        if (questManager.quests[questIndex].IsCompleted)
        {
            // If the quest is completed, deactivate all its clues and return
            for (int j = 0; j < obj.Count; j++)
            {
                if (obj[j].gameObject != null)
                    obj[j].gameObject.SetActive(false);
            }
            return;
        }
    }

    private void CheckForTimeTravel(List<Clues> obj)
    {
        // Existing code
        if (_timeTravelSystem.canTravel == true && _timeTravelSystem.effectActivated == true)
        {
            foreach (var clues in obj)
            {
                if (clues.clues != null) // Check if clues is not null
                {
                    foreach (var clue in clues.clues)
                        clue.SetActive(true);
                }
            }
        }
        else
        {
            foreach (var clues in obj)
            {
                if (clues.clues != null) // Check if clues is not null
                {
                    foreach (var clue in clues.clues)
                        clue.SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if (obj != null)
        {
            CheckForTimeTravel(obj);

            // Check if the current active clue is completed
            foreach (var clueGroup in obj)
            {
                foreach (var clue in clueGroup.clues)
                {
                    if (clue.activeSelf && !completedClues.Contains(clue))
                    {
                        var noteSystem = clue.GetComponent<NoteAppearing_System>();
                        var slidingGame = clue.GetComponent<PuzzleSystem>();

                        if ((noteSystem != null && noteSystem.IsQuestCompleted()) ||
                            (slidingGame != null && slidingGame.IsQuestCompleted()))
                        {
                            // If the clue is completed, set the objective to true
                            questManager.SetObjectiveCompletion(questManager.GetActiveQuestIndex(), questManager.quests[questManager.GetActiveQuestIndex()].GetActiveObjectiveIndex(), true);

                            // Add the completed clue to the set
                            completedClues.Add(clue);

                            return; // return early as the objective is completed
                        }
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class ClueGroup
{
    public List<Clues> clues;
}