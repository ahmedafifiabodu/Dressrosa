using System.Collections.Generic;
using UnityEngine;

public class ClueSystem : MonoBehaviour
{
    [Header("Quest Objectives")]
    [SerializeField] private List<ClueGroup> questObjectives;

    internal InputManager _inputManager;
    private QuestManager _questManager;
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

    private void Start()
    {
        _timeTravelSystem = TimeTravelSystem.Instance;
        _inputManager = InputManager.Instance;
        _questManager = QuestManager.Instance;
    }

    internal void ActiveObjectives(int questIndex)
    {
        if (questObjectives == null || questIndex >= questObjectives.Count || questIndex < 0)
            return;

        obj = questObjectives[questIndex].clues;

        if (_questManager.quests[questIndex].IsCompleted)
        {
            for (int j = 0; j < obj.Count; j++)
            {
                if (obj[j].gameObject != null)
                    obj[j].gameObject.SetActive(false);
            }
            return;
        }

        for (int j = 0; j < obj.Count; j++)
        {
            if (obj[j].gameObject != null)
                obj[j].gameObject.SetActive(false);
        }

        // Activate the first clue in the first Clues object that is not completed
        foreach (var clueGroup in obj)
        {
            foreach (var clue in clueGroup.clues)
            {
                if (!clue.activeSelf && !completedClues.Contains(clue))
                {
                    clue.SetActive(true);
                    return;
                }
            }
        }
    }

    private void CheckForTimeTravel(List<Clues> obj)
    {
        if (_timeTravelSystem.canTravel == true && _timeTravelSystem.effectActivated == true)
        {
            // Find the first inactive clue
            GameObject firstInactiveClue = null;
            foreach (var clues in obj)
            {
                if (clues.clues != null) // Check if clues is not null
                {
                    foreach (var clue in clues.clues)
                    {
                        if (!clue.activeSelf && !completedClues.Contains(clue))
                        {
                            firstInactiveClue = clue;
                            break;
                        }
                    }
                }
                if (firstInactiveClue != null)
                    break;
            }

            // Activate the first inactive clue
            if (firstInactiveClue != null)
                firstInactiveClue.SetActive(true);
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
            for (int i = 0; i < obj.Count; i++)
            {
                var clueGroup = obj[i];
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
                            _questManager.SetObjectiveCompletion(_questManager.GetActiveQuestIndex(), _questManager.quests[_questManager.GetActiveQuestIndex()].GetActiveObjectiveIndex(), true);

                            // Add the completed clue to the set
                            completedClues.Add(clue);

                            // Deactivate the current clue
                            clue.SetActive(false);

                            // If the clue has a PuzzleSystem, deactivate the puzzle GameObject
                            if (slidingGame != null)
                                slidingGame.puzzle.SetActive(false);

                            // Activate the next clue
                            if (i + 1 < obj.Count && obj[i + 1].gameObject != null)
                                obj[i + 1].gameObject.SetActive(true);

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