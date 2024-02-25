using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    [SerializeField] private Transform teleportPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
            collision.gameObject.transform.position = teleportPosition.position;
    }
}