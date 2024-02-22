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

    private void Update()
    {
        if (_currentInteractable != null && _inputManager._playerInput.Player.Interact.triggered)
            _currentInteractable.BaseInteract();

        if (_currentDialog != null && _inputManager._playerInput.Player.Interact.triggered)
            _currentDialog.OnInteract();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int activeQuestIndex = questManager.GetActiveQuestIndex();

        if (collision.TryGetComponent<Interactable>(out var _interactable))
        {
            if (collision.TryGetComponent<DialogController>(out var _dialogController))
            {
                Debug.Log("Active Quest Index: " + activeQuestIndex);
                Debug.Log("DialogController enabled: " + _dialogController.enabled);
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
            {
                _currentDialog = null;
            }
        }
    }
}