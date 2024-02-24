using UnityEngine;

public class PuzzleSystem : MonoBehaviour
{
    [SerializeField] internal GameObject puzzle;
    [SerializeField] private SildingGameManager _sildingPuzzle;
    [SerializeField] private StonePuzzle _stonePuzzle;
    [SerializeField] private NoteAppearingSystem _noteAppearingSystem;

    private bool noteActive;
    private bool canSeeNote;

    private ClueSystem _clueSystem;

    private void Start()
    {
        noteActive = false;
        canSeeNote = false;

        _clueSystem = ClueSystem.Instance;
    }

    private void Update()
    {
        if (_clueSystem._inputManager._playerInput.Player.Interact.triggered
            && canSeeNote == true)
            noteActive = !noteActive;

        ActivateGameObject();
    }

    public bool IsQuestCompleted()
    {
        if (_sildingPuzzle == null)
            return _sildingPuzzle.IsQuestCompleted();

        if (_stonePuzzle == null)
            return _stonePuzzle.IsQuestCompleted();

        if (_noteAppearingSystem == null)
            return _noteAppearingSystem.IsQuestCompleted();

        return false;
    }

    private void ActivateGameObject()
    {
        if (noteActive == true)
            puzzle.SetActive(true);
        else if (noteActive == false)
            puzzle.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
            canSeeNote = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
        {
            canSeeNote = false;
            noteActive = false;
        }
    }
}