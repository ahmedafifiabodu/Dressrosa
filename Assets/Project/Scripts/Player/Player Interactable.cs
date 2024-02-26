using TMPro;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _promptMessage;

    private InputManager _inputManager;
    private QuestManager _questManager;
    private DialogManager _DialogManager;

    private Interactable _currentInteractable;
    private DialogController _currentDialog;
    private InventoryParameters _inventoryParameters;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        _inputManager = InputManager.Instance;
        _DialogManager = DialogManager.Instance;
        _questManager = QuestManager.Instance;
    }

    private void Update()
    {
        if (_currentInteractable != null && _inputManager._playerInput.Player.Interact.triggered)
            _currentInteractable.BaseInteract();

        if (_currentDialog != null && _inputManager._playerInput.Player.Interact.triggered)
            _currentDialog.OnInteract();

        if (_inventoryParameters != null && _inputManager._playerInput.Player.Interact.triggered)
        {
            // Check if there is an available slot
            Slot[] slots = _inventoryParameters._inventoryManger._inventoryCanvas.GetComponentsInChildren<Slot>();
            Slot availableSlot = System.Array.Find(slots,
                slot => slot.ids.Contains(_inventoryParameters._slotId) && !slot.IsOccupied);

            // If there is an available slot, add the item to the inventory and deactivate the game object
            if (availableSlot != null)
            {
                _inventoryParameters._inventoryManger.AddItemToInventory(_inventoryParameters, availableSlot);
                _inventoryParameters.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactable>(out var _interactable))
        {
            if (collision.TryGetComponent<DialogController>(out var _dialogController))
            {
                // If there's an active quest and the DialogController is enabled, allow interaction
                if (_dialogController.enabled)
                {
                    _audioManager.PlaySFX(_audioManager.interact);

                    _promptMessage.gameObject.SetActive(true);
                    _promptMessage.text = _interactable._promptMessage;
                    _currentInteractable = _interactable;

                    if (_DialogManager.currentDialogIndex >= 0 && _DialogManager.currentDialogIndex < _DialogManager.dialogComponents.Count)
                        _DialogManager.dialogComponents[_DialogManager.currentDialogIndex].Mark.SetActive(true);

                    _currentDialog = _dialogController;
                }
            }
            else if (collision.TryGetComponent<InventoryParameters>(out var _inventoryParameter))
            {
                _audioManager.PlaySFX(_audioManager.interact);

                _promptMessage.gameObject.SetActive(true);
                _promptMessage.text = _interactable._promptMessage;
                _currentInteractable = _interactable;
                _inventoryParameters = _inventoryParameter;
            }
            else
            {
                _audioManager.PlaySFX(_audioManager.interact);

                _promptMessage.gameObject.SetActive(true);
                _promptMessage.text = _interactable._promptMessage;
                _currentInteractable = _interactable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactable>(out _))
        {
            if (_promptMessage != null)
            {
                _promptMessage.text = string.Empty;
                _promptMessage.gameObject.SetActive(false);
            }
            _currentInteractable = null;
        }

        if (collision.TryGetComponent<DialogController>(out _))
        {
            if (_currentDialog == collision.GetComponent<DialogController>())
                _currentDialog = null;
        }
    }
}