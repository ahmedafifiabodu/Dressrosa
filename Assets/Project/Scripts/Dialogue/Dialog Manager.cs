using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] internal List<DialogComponent> dialogComponents = new();

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    internal bool IsDialogActive { get; set; }
    internal int currentDialogIndex = 0;

    private void Start()
    {
        // Start the first dialog and deactivate all others
        for (int i = 0; i < dialogComponents.Count; i++)
        {
            dialogComponents[i].Controller.enabled = (i == 0);
            dialogComponents[i].Mark.SetActive((i == 0));
        }
    }

    internal void OnDialogComplete()
    {
        // Deactivate the current dialog
        if (currentDialogIndex < dialogComponents.Count)
        {
            dialogComponents[currentDialogIndex].Controller.enabled = false;
            dialogComponents[currentDialogIndex].Mark.SetActive(false);
        }

        // Increment the currentDialogIndex
        currentDialogIndex++;

        // Activate the next dialog
        if (currentDialogIndex < dialogComponents.Count)
            dialogComponents[currentDialogIndex].Controller.enabled = true;

        IsDialogActive = false;
    }

    internal void OnDialogStart() => IsDialogActive = true;
}

[System.Serializable]
public class DialogComponent
{
    [SerializeField] private DialogController controller;
    [SerializeField] private GameObject mark;

    public DialogController Controller { get => controller; set => controller = value; }
    public GameObject Mark { get => mark; set => mark = value; }
}