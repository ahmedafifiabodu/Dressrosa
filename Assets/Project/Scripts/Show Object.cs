using UnityEngine;

public class ShowObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToShow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the other object has the tag "Player"
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
            objectToShow.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the other object has the tag "Player"
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
            objectToShow.SetActive(false);
    }
}