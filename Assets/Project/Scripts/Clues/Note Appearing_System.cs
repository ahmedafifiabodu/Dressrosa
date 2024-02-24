using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteAppearingSystem : MonoBehaviour
{
    [Header("Note")]
    [SerializeField] private Image noteImage;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField][TextArea] private string noteText;

    private bool _isQuestCompleted;
    private bool noteActive;
    private bool canSeeNote;

    private ClueSystem _clueSystem;

    internal bool IsQuestCompleted() => _isQuestCompleted;

    private void Start()
    {
        _isQuestCompleted = false;
        text.text = noteText;
        noteActive = false;
        canSeeNote = false;

        _clueSystem = ClueSystem.Instance;
    }

    private void Update()
    {
        if (_clueSystem._inputManager._playerInput.Player.Interact.triggered && canSeeNote == true)
        {
            noteActive = !noteActive;
            _isQuestCompleted = true;
        }

        ActivateNote();
    }

    private void ActivateNote()
    {
        if (noteActive == true)
            noteImage.gameObject.SetActive(true);
        else if (noteActive == false)
            noteImage.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
            canSeeNote = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
        {
            canSeeNote = false;
            noteActive = false;
        }
    }
}