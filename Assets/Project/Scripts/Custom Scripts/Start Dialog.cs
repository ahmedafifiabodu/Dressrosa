using UnityEngine;

public class StartDialog : MonoBehaviour
{
    private NPC character;
    private bool hasTriggered = false;

    private void Start() => character = GetComponent<NPC>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
        {
            character.GoToTargetPoint();
            hasTriggered = true;
        }
    }
}