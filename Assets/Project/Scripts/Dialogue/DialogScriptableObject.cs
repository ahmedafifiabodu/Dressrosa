using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Object", menuName = "Scriptable Object/Dialog System")]
public class DialogScriptableObject : ScriptableObject
{
    [SerializeField] private event System.Action DialogComplete;

    [SerializeField] private List<DialogItems> dialogItems;
    [SerializeField] private bool isLoopable;
    [SerializeField] private bool isOrdered;
    [SerializeField] private DialogScriptableObject nextDialog;

    protected bool hasCompleted;
    protected Queue<DialogItems> orderedDialog;

    public virtual DialogItems GetNextDialog()
    {
        if (isOrdered)
        {
            if (orderedDialog.Count > 0)
                return orderedDialog.Dequeue();
            else
            {
                if (isLoopable)
                {
                    BuildTextQueue();
                    return orderedDialog.Dequeue();
                }
                else
                {
                    OnDialogComplete();
                    return null;
                }
            }
        }

        // Return Random Index if not orderable
        int randomIndex = Random.Range(0, dialogItems.Count);
        return dialogItems[randomIndex];
    }

    protected virtual void OnDialogComplete() => DialogComplete?.Invoke();

    private void OnEnable()
    {
        if (isOrdered)
            BuildTextQueue();
    }

    private void BuildTextQueue()
    {
        orderedDialog = new Queue<DialogItems>();

        foreach (DialogItems item in dialogItems)
            orderedDialog.Enqueue(item);
    }
}

[System.Serializable]
public class DialogItems
{
    [SerializeField][TextArea] private string dialogText;
    [SerializeField] private AudioClip sound;
    [SerializeField] private Texture2D panel;
    [SerializeField] private bool leftWrite;

    public string DialogText { get => dialogText; set => dialogText = value; }
    public AudioClip Sound { get => sound; set => sound = value; }
    public Texture2D Panel { get => panel; set => panel = value; }
    public bool LeftWrite { get => leftWrite; set => leftWrite = value; }
}