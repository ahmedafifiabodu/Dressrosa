using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puzzle_System : MonoBehaviour
{
    [SerializeField] private GameObject puzzle;
    [SerializeField] private SildingGameMAnager slidePuzzle;
    private bool noteActive;
    private bool canSeeNote;

    private void Start()
    {
        noteActive = false;
        canSeeNote = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSeeNote == true)
        {
            noteActive = !noteActive;
        }

        ActivateGameObject();
    }

    private void ActivateGameObject()
    {
        if (noteActive == true)
        {
            puzzle.gameObject.SetActive(true);
        }
        else if (noteActive == false)
        {
            puzzle.gameObject.SetActive(false);
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
