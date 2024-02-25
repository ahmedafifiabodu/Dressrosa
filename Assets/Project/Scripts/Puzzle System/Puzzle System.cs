using UnityEngine;

public class PuzzleSystem : MonoBehaviour
{
    [SerializeField] private SildingGameManager _sildingPuzzle;
    [SerializeField] private StonePuzzle _stonePuzzle;
    [SerializeField] private NoteAppearingSystem _noteAppearingSystem;

    public bool IsQuestCompleted()
    {
        if (_sildingPuzzle != null)
            return _sildingPuzzle.IsQuestCompleted();

        if (_stonePuzzle != null)
            return _stonePuzzle.IsQuestCompleted();

        if (_noteAppearingSystem != null)
            return _noteAppearingSystem.IsQuestCompleted();

        return false;
    }
}