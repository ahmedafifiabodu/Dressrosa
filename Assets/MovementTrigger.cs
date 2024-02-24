using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTrigger : MonoBehaviour
{
    [SerializeField] private PlayerIsometricMovement player;
    [SerializeField] private GameObject wall;

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
			wall.SetActive(true);
			player._invertDirection = true;
            canSwithMovement = false;
            
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("Trigger Exit");
		if (collision.gameObject.CompareTag("Player") && canSwithMovement == true)
		{
			player._invertDirection = false;
			canSwithMovement = false;
			
		}
	}
}
