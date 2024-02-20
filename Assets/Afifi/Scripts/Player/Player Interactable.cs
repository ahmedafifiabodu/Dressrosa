using TMPro;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private TextMeshProUGUI _promptMessage;

    private Interactable _currentInteractable;

    private void Update()
    {
        if (_currentInteractable != null && _inputManager._playerInput.Player.Interact.triggered)
        {
            Debug.Log("Interacting with " + _currentInteractable.name);
            _currentInteractable.BaseInteract();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactable>(out var _interactable))
        {
            _promptMessage.gameObject.SetActive(true);
            _promptMessage.text = _interactable._promptMessage;
            _currentInteractable = _interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Interactable>(out _))
        {
            _promptMessage.text = string.Empty;
            _promptMessage.gameObject.SetActive(false);
            _currentInteractable = null;
        }
    }
}