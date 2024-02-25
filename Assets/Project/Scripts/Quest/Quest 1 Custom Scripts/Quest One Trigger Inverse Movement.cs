using System.Collections;
using UnityEngine;

public class QuestOneTriggerInverseMovement : MonoBehaviour
{
    [SerializeField] private PlayerIsometricMovement player;
    [SerializeField] private int timeToWaitForInvertMomentToTriggered;
    [SerializeField] private GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
            StartCoroutine(WaitAndInvertDirection(
                true,
                timeToWaitForInvertMomentToTriggered));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG) && gameObject.activeInHierarchy)
            player._invertDirection = false;
    }

    private IEnumerator WaitAndInvertDirection(bool setInvertDirection, int seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (wall != null)
            wall.SetActive(true);

        player._invertDirection = setInvertDirection;
    }
}