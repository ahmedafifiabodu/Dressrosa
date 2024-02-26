using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerIsometricMovement _playerMovement;
    [SerializeField] private QuestManager _questManager;
    [SerializeField] private InventoryManger _inventoryManager;
    [SerializeField] private TimeTravelSystem _timeTravelSystem;

    internal InputSystem _playerInput;
    private InputSystem.PlayerActions _playerActions;
    private AudioManager audioManager;

    private bool isQuestOpen = false;
    private bool isInventoryOpen = false;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioManager = AudioManager.Instance;

        _playerInput = new InputSystem();

        _playerActions = _playerInput.Player;

        _playerActions.Fire.performed += _ => _playerMovement.Fire();

        _playerActions.Movement.performed += _ => audioManager.PlayWalkSFX(audioManager.walk);

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