using System.Collections.Generic;
using UnityEngine;

public class ClueSystem : MonoBehaviour
{
    [SerializeField] private List<ClueGroup> questObjectives;

    internal InputManager _inputManager;
    private QuestManager _questManager;
    private TimeTravelSystem _timeTravelSystem;

    private List<Clues> obj;
    private readonly HashSet<Clue> completedClues = new();
    public static ClueSystem Instance { get; private set; }

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

        // Deactivate all clues
        foreach (var clueGroup in obj)
        {
            foreach (var clue in clueGroup.clues)
            {
                if (clue.clue != null && clue.deactivateAtStart)
                    clue.clue.SetActive(false);
            }
        }

        // Activate the first clue in the first Clues object that is not completed
        foreach (var clueGroup in obj)
        {
            foreach (var clue in clueGroup.clues)
            {
                if (!clue.clue.activeSelf && !completedClues.Contains(clue))
                {
                    clue.clue.SetActive(true);
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
            Clue firstInactiveClue = null;
            foreach (var clues in obj)
            {
                if (clues.clues != null) // Check if clues is not null
                {
                    foreach (var clue in clues.clues)
                    {
                        if (!clue.clue.activeSelf && !completedClues.Contains(clue))
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
                firstInactiveClue.clue.SetActive(true);
        }
        else
        {
            foreach (var clues in obj)
            {
                if (clues.clues != null) // Check if clues is not null
                {
                    foreach (var clue in clues.clues)
                        clue.clue.SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if (obj != null)
        {
            // Check if the current active clue is completed
            for (int i = 0; i < obj.Count; i++)
            {
                var clueGroup = obj[i];
                foreach (var clue in clueGroup.clues)
                {
                    if (clue.clue.activeSelf && !completedClues.Contains(clue))
                    {
                        var _puzzleGame = clue.clue.GetComponent<PuzzleSystem>();
                        var _goal = clue.clue.GetComponent<GoalSystem>();

                        if (_puzzleGame != null && _puzzleGame.IsQuestCompleted() ||
                            _goal != null && _goal.IsGoalCompleted())
                        {
                            // If the clue is completed, set the objective to true
                            _questManager.SetObjectiveCompletion(
                                _questManager.GetActiveQuestIndex(),
                                _questManager.quests[_questManager.GetActiveQuestIndex()].GetActiveObjectiveIndex(),
                                true);

                            // Add the completed clue to the set
                            completedClues.Add(clue);

                            // Deactivate the current clue
                            if (!clue.remainActiveAfterCompletion)
                                clue.clue.SetActive(false);

                            // If the clue has a PuzzleSystem, deactivate the puzzle GameObject
                            if (_puzzleGame != null && _puzzleGame.puzzle != null)
                                _puzzleGame.puzzle.SetActive(false);

                            // Activate the next clue
                            if (i + 1 < obj.Count && obj[i + 1].clues.Count > 0)
                                obj[i + 1].clues[0].clue.SetActive(true);

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
    [SerializeField] internal List<Clues> clues;
}