using UnityEngine;

public class StonePuzzle : MonoBehaviour
{
    [SerializeField] private NPC[] _npcs;
    [SerializeField] private GameObject[] _objectsToDisableAfterPuzzleIsFinished;
    [SerializeField] private int _stonePuzzleCounterToCompleteThePuzzle;

    internal int quset1_Counter;
    private bool _isQuestCompleted = false;
    private bool _hasStartedMoving = false;

    private PlayerIsometricMovement _playerMovement;

    public bool IsQuestCompleted() => _isQuestCompleted;

    private void Start()
    {
        _playerMovement = PlayerIsometricMovement.Instance;
        quset1_Counter = 0;
    }

    private void Update()
    {
        if (quset1_Counter == _stonePuzzleCounterToCompleteThePuzzle && !_hasStartedMoving)
        {
            _playerMovement._invertDirection = false;
            _isQuestCompleted = true;

            foreach (var NPC in _npcs)
            {
                NPC.gameObject.SetActive(true);
                NPC.GoToTargetPoint();
            }

            foreach (var obj in _objectsToDisableAfterPuzzleIsFinished)
                obj.SetActive(false);

            _hasStartedMoving = true;
        }
    }
}