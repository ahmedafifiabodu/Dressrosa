using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog : MonoBehaviour
{
    [SerializeField] private NPC character;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            character.startMovement();
            Destroy(this.gameObject);
        }
    }
}
