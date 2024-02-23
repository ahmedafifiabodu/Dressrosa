using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrigger : MonoBehaviour
{
    [SerializeField] private PlayerIsometricMovement player;
    private bool canSwithMovement;
    // Start is called before the first frame update

    private void Start()
    {
        canSwithMovement = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canSwithMovement == true)
        {
            player._invertDirection = true;
            canSwithMovement = false;
            StartCoroutine(waitTime());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canSwithMovement == true)
        {
            player._invertDirection = false;
            canSwithMovement = false;
            StartCoroutine(waitTime());
        }
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(4);
        canSwithMovement = true;
    }
}
