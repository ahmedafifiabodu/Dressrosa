using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] internal Sprite icon;
    [SerializeField] internal string questName;
    [SerializeField] internal List<Objective> objectives = new();

    internal bool IsCompleted
    {
        get
        {
            foreach (var objective in objectives)
            {
                if (!objective.isCompleted)
                    return false;
            }
            return true;
        }
    }

    internal int GetActiveObjectiveIndex()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (!objectives[i].isCompleted)
                return i;
        }
        return -1;
    }
}

[System.Serializable]
public class Objective
{
    [SerializeField] internal string objectiveName;
    [SerializeField][TextArea] internal string description;
    [SerializeField] internal bool isCompleted;
}