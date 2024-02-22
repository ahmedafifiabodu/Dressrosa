using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteAppearing_System : MonoBehaviour
{
    [SerializeField] private QuestManager _quest;
    [SerializeField] private Image noteImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string noteText;
    private bool QuestCompleted;
    private bool noteActive;
    private bool canSeeNote;

    private void Start()
    {
        QuestCompleted = false;
        text.text = noteText;
        noteActive = false;
        canSeeNote = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canSeeNote == true)
        {
            noteActive = !noteActive;
            if(QuestCompleted == false)
            {
                _quest.SetObjectiveCompletion(Clue_System.instance.questIndex, Clue_System.instance.objectiveIndex, true);
                QuestCompleted = true;
            }
        }

        ActivateNote();
    }

    private void ActivateNote()
    {
        if(noteActive == true)
        {
            noteImage.gameObject.SetActive(true);
        }
        else if(noteActive == false)
        {
            noteImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canSeeNote = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canSeeNote = false;
            noteActive = false;
        }
    }
}
