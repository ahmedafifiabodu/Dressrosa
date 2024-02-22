using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] internal InputManager _inputManager;
    [SerializeField] internal List<DialogComponent> dialogComponents = new();

    public bool IsDialogActive { get; private set; }
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
        {
			dialogComponents[currentDialogIndex].Controller.enabled = true;
			//dialogComponents[currentDialogIndex].Mark.SetActive(true);
		}

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


/*
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	[SerializeField] internal InputManager _inputManager;
	[SerializeField] private List<DialogComponent> dialogComponents = new List<DialogComponent>();
	public bool IsDialogActive { get; private set; }
	private int currentDialogIndex = 0;

	private void Start()
	{
		// Initialize dialog components
		foreach (var dialogComponent in dialogComponents)
		{
			dialogComponent.Controller.enabled = false;
			dialogComponent.Mark.SetActive(false);
		}

		// Activate the first dialog
		if (dialogComponents.Count > 0)
		{
			dialogComponents[currentDialogIndex].Controller.enabled = true;
			dialogComponents[currentDialogIndex].Mark.SetActive(true);
			IsDialogActive = true;
		}
	}

	internal void OnDialogComplete()
	{
		if (currentDialogIndex < dialogComponents.Count)
		{
			dialogComponents[currentDialogIndex].Controller.enabled = false;
			dialogComponents[currentDialogIndex].Mark.SetActive(false);
		}

		currentDialogIndex++;

		if (currentDialogIndex < dialogComponents.Count)
		{
			dialogComponents[currentDialogIndex].Controller.enabled = true;
			dialogComponents[currentDialogIndex].Mark.SetActive(true);
			IsDialogActive = true;
		}
		else
		{
			IsDialogActive = false;
		}
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
}*/
