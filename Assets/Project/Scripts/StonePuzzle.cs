using UnityEngine;

public class StonePuzzle : MonoBehaviour
{
    [SerializeField] private NPC[] charater;
    [SerializeField] private GameObject stone, note, _wireWall;

    public static int quset1_Counter;
    private bool _isQuestCompleted = false;

    private PlayerIsometricMovement player;

    public bool IsQuestCompleted() => _isQuestCompleted;

    private void Start()
    {
        quset1_Counter = 0;
        stone.SetActive(false);
        note.SetActive(false);

        player = PlayerIsometricMovement.Instance;
    }

    private void Update()
    {
        if (quset1_Counter == 6)
        {
            player._invertDirection = false;
            _isQuestCompleted = true;

            for (int i = 0; i < charater.Length; i++)
                charater[i].StartMovement();

            stone.SetActive(true);
            note.SetActive(true);

            Destroy(_wireWall);
        }
    }
}