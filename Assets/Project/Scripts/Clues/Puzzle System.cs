using UnityEngine;

public class PuzzleSystem : MonoBehaviour
{
    [SerializeField] private QuestManager _quest;
    [SerializeField] private GameObject puzzle;
    [SerializeField] private SildingGameManager _sildingPuzzle;

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
        if (_clueSystem._inputManager._playerInput.Player.Interact.triggered && canSeeNote == true)
            noteActive = !noteActive;

        ActivateGameObject();
    }

    public bool IsQuestCompleted() => _sildingPuzzle._isQuestCompleted;

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