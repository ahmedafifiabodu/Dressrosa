using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Object", menuName = "Scriptable Object/Dialog System")]
public class DialogScriptableObject : ScriptableObject
{
    [SerializeField] private event System.Action DialogComplete;

    [SerializeField] private List<string> DialogText;
    [SerializeField] private bool IsLoopable;
    [SerializeField] private bool IsOrdered;
    [SerializeField] private DialogScriptableObject NextDialog;

    protected string _dialogID;
    protected bool _hasCompleted;
    protected Queue<string> _ordededDialog;

    public virtual string GetNextDialog()
    {
        if (IsOrdered)
        {
            if (_ordededDialog.Count > 0)
                return _ordededDialog.Dequeue();
            else
            {
                if (IsLoopable)
                {
                    BuildTextQueue();
                    return _ordededDialog.Dequeue();
                }
                else
                {
                    OnDialogComplete();
                    return "";
                }
            }
        }

        // Return Random Index if not orderable
        int randomIndex = Random.Range(0, DialogText.Count);
        return DialogText[randomIndex];
    }

    protected virtual void OnDialogComplete() => DialogComplete?.Invoke();

    private void OnEnable()
    {
        if (IsOrdered)
            BuildTextQueue();
    }

    private void BuildTextQueue()
    {
        _ordededDialog = new Queue<string>();

        for (int i = 0; i < DialogText.Count; i++)
            _ordededDialog.Enqueue(DialogText[i]);
    }
}