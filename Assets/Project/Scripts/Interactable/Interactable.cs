using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool _useEvents;
    public string _promptMessage;

    private AudioManager _audioManager;

    private void Start() => _audioManager = AudioManager.Instance;

    protected virtual string OnLook() => _promptMessage;

    internal void BaseInteract()
    {
        if (_useEvents)
        {
            _audioManager.PlaySFX(_audioManager.wallTouch);

            if (gameObject.TryGetComponent<InteractableEvents>(out var _events))
                _events.onInteract.Invoke();
        }
        else
            Interact();
    }

    protected virtual void Interact() => Debug.Log($"(Virtual) Interacting with {gameObject.name}");
}