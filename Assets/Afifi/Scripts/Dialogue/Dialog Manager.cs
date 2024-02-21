using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] internal InputManager _inputManager;
    [SerializeField] private List<DialogController> dialogControllers = new();

    public bool IsDialogActive { get; private set; }
    private int currentDialogIndex = 0;

    private void Start()
    {
        // Start the first dialog and deactivate all others
        for (int i = 0; i < dialogControllers.Count; i++)
            dialogControllers[i].enabled = (i == 0);
    }

    internal void OnDialogComplete()
    {
        // Deactivate the current dialog
        if (currentDialogIndex < dialogControllers.Count)
        {
            dialogControllers[currentDialogIndex].enabled = false;
        }

        // Increment the currentDialogIndex
        currentDialogIndex++;

        // Activate the next dialog
        if (currentDialogIndex < dialogControllers.Count)
        {
            dialogControllers[currentDialogIndex].enabled = true;
        }

        IsDialogActive = false;
    }

    internal void OnDialogStart() => IsDialogActive = true;
}