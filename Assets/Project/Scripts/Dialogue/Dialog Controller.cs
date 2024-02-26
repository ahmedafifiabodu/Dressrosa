using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private DialogScriptableObject CurrentDialogObject;
    [SerializeField] private DialogUI _DialogUI;
    [SerializeField] internal bool shouldStartQuestAfterDialog;
    [SerializeField] private int questIndexToStart;

    private QuestManager _questManager;
    private DialogManager _dialogManager;

    private void Start()
    {
        _questManager = QuestManager.Instance;
        _dialogManager = DialogManager.Instance;
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

            // Check if a quest should start after the dialog
            if (shouldStartQuestAfterDialog) // Add this line
            {
                _questManager.StartQuest(questIndexToStart);
                _dialogManager.OnDialogComplete();
            }
            else
            {
                GetComponent<DialogController>().enabled = false;
                _dialogManager.IsDialogActive = false;
            }

            return;
        }

        // Show the next dialogue
        _DialogUI.ShowText(nextDialog, true);
    }
}