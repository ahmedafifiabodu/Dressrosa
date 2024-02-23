using TMPro;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private TextMeshProUGUI _promptMessage;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private DialogManager _DialogManager;

    private Interactable _currentInteractable;
    private DialogController _currentDialog;
    private InventoryParameters _inventoryParameters;
    private AudioManager audioManager;

    private void Start() => audioManager = AudioManager.Instance;

    private void Update()
    {
        if (_currentInteractable != null && _inputManager._playerInput.Player.Interact.triggered)
        {
            audioManager.PlaySFX(audioManager.interact);
            _currentInteractable.BaseInteract();
        }

        if (_currentDialog != null && _inputManager._playerInput.Player.Interact.triggered)
        {
            audioManager.PlaySFX(audioManager.interact);
            _currentDialog.OnInteract();
        }

        if (_inventoryParameters != null && _inputManager._playerInput.Player.Interact.triggered)
        {
            audioManager.PlaySFX(audioManager.interact);

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
        int activeQuestIndex = questManager.GetActiveQuestIndex();

        if (collision.TryGetComponent<Interactable>(out var _interactable))
        {
            if (collision.TryGetComponent<DialogController>(out var _dialogController))
            {
                // If there's an active quest and the DialogController is enabled, allow interaction
                if (activeQuestIndex == -1 && _dialogController.enabled)
                {
                    _promptMessage.gameObject.SetActive(true);
                    _promptMessage.text = _interactable._promptMessage;
                    _currentInteractable = _interactable;
                    _DialogManager.dialogComponents[_DialogManager.currentDialogIndex].Mark.SetActive(true);
                    _currentDialog = _dialogController;
                }
            }
            else if (collision.TryGetComponent<InventoryParameters>(out var _inventoryParameter))
            {
                _promptMessage.gameObject.SetActive(true);
                _promptMessage.text = _interactable._promptMessage;
                _currentInteractable = _interactable;
                _inventoryParameters = _inventoryParameter;
            }
            else
            {
                // If there's no DialogController, allow interaction regardless of whether there's an active quest
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