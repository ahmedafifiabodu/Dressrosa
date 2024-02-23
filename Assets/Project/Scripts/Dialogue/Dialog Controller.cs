using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] internal DialogManager dialogManager;
    [SerializeField] private DialogUI DialogUI;
    [SerializeField] private DialogScriptableObject CurrentDialogObject;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private int questIndexToStart;
    [SerializeField] private List<GameObject> objectsToEnable; // List of objects to enable

    private void OnEnable() => DialogUI.NextButton.onClick.AddListener(OnInteract);

    private void OnDisable() => DialogUI.NextButton.onClick.RemoveListener(OnInteract);

    internal void OnInteract()
    {
        // Call OnDialogStart when a dialog starts
        dialogManager.OnDialogStart();

        // Get the next dialogue
        DialogItems nextDialog = CurrentDialogObject.GetNextDialog();

        // Check if next dialogue is empty
        if (nextDialog == null)
        {
            // Close the dialogue
            DialogUI.gameObject.SetActive(false);

            // Notify the quest manager to start a quest
            questManager.StartQuest(questIndexToStart);

            // Notify the dialog manager that the dialog is complete
            dialogManager.OnDialogComplete();

            // Enable the objects
            if (objectsToEnable != null && objectsToEnable.Count > 0)
                foreach (var obj in objectsToEnable)
                    obj.SetActive(true);

            return;
        }

        // Show the next dialogue
        DialogUI.ShowText(nextDialog, true);
    }
}