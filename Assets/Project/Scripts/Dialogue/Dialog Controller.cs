using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private DialogScriptableObject CurrentDialogObject;
    [SerializeField] private DialogUI _DialogUI;
    [SerializeField] private int questIndexToStart;

    private QuestManager _questManager;
    private DialogManager _dialogManager;
    private ClueSystem _clueSystem;

    private void Start()
    {
        _questManager = QuestManager.Instance;
        _dialogManager = DialogManager.Instance;
        _clueSystem = ClueSystem.Instance;
    }

    private void OnEnable() => _DialogUI.NextButton.onClick.AddListener(OnInteract);

    private void OnDisable() => _DialogUI.NextButton.onClick.RemoveListener(OnInteract);

    internal void OnInteract()
    {
        // Call OnDialogStart when a dialog starts
        _dialogManager.OnDialogStart();

        // Get the next dialogue
        DialogItems nextDialog = CurrentDialogObject.GetNextDialog();

        // Check if next dialogue is empty
        if (nextDialog == null)
        {
            // Close the dialogue
            _DialogUI.gameObject.SetActive(false);

            // Notify the dialog manager that the dialog is complete
            _dialogManager.OnDialogComplete();

            // Notify the quest manager to start a quest
            _questManager.StartQuest(questIndexToStart);

            // Activate the clues for the quest
            _clueSystem.ActiveObjectives(questIndexToStart);

            return;
        }

        // Show the next dialogue
        _DialogUI.ShowText(nextDialog, true);
    }
}