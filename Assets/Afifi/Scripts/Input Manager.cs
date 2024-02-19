using UnityEngine;

public class Inputmanager : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private QuestManager _questManager;

    private InputSystem _playerInput;
    private InputSystem.PlayerActions _playerActions;

    private void Awake()
    {
        _playerInput = new InputSystem();

        _playerActions = _playerInput.Player;

        _playerActions.Fire.performed += _ => _playerMovement.Fire();
        _playerActions.Quest.performed += _ => _questManager.SetActiveForQuestPanel(true);


        /*        _onFoot.Switch.performed += ctx =>
                {
                    var scroll = ctx.ReadValue<float>();

                    if (scroll > 0)
                        _playerShooting.SwitchToNextWeapon();
                    else if (scroll < 0)
                        _playerShooting.SwitchToPreviousWeapon();
                };*/
    }

    private void FixedUpdate() => _playerMovement.ProcessMove(_playerActions.Movement.ReadValue<Vector2>());

    private void OnEnable() => _playerInput.Enable();

    private void OnDisable() => _playerInput.Disable();
}