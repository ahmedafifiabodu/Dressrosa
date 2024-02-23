using UnityEngine;


public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerIsometricMovement _playerMovement;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private InventoryManger _inventoryManager;
    [SerializeField] private TimeTravelSystem _timeTravelSystem;

    internal InputSystem _playerInput;
    private InputSystem.PlayerActions _playerActions;

    private bool isQuestOpen = false;
    private bool isInventoryOpen = false;

    private void Awake()
    {
        _playerInput = new InputSystem();

        _playerActions = _playerInput.Player;

        _playerActions.Fire.performed += _ => _playerMovement.Fire();

        _playerActions.Movement.performed += _ => AudioManager.Instance.PlayWalkSFX(AudioManager.Instance.walk);

        _playerActions.Quest.performed += _ =>
        {
            if (!isInventoryOpen)
            {
                isQuestOpen = !isQuestOpen;
                _questManager.SetActiveForQuestPanel(isQuestOpen);
            }
        };

        _playerActions.Inventory.performed += _ =>
        {
            if (!isQuestOpen)
            {
                isInventoryOpen = !isInventoryOpen;
                _inventoryManager.SetActiveForInventoryPanel(isInventoryOpen);
            }
        };

        _playerActions.TimeTravel.performed += _ => _timeTravelSystem.ActiveTimeTravel();
    }

    private void FixedUpdate() => _playerMovement.ProcessMove(_playerActions.Movement.ReadValue<Vector2>());

    private void OnEnable() => _playerInput.Enable();

    private void OnDisable() => _playerInput.Disable();
}