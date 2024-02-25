using System.Collections.Generic;
using UnityEngine;

public class Clues : MonoBehaviour
{
    [SerializeField] internal List<Clue> clues;
}

[System.Serializable]
public class Clue
{
    [SerializeField] internal GameObject clue;
    [SerializeField] internal bool remainActiveAfterCompletion;
    [SerializeField] internal bool deactivateAtStart;
}