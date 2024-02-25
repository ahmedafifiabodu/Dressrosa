using UnityEngine;

public class StonePuzzleCollider : MonoBehaviour
{
    [SerializeField] private StonePuzzle stonePuzzle;
    private Rigidbody2D rb;

    private void Start() => rb = GetComponent<Rigidbody2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.FOGCIRCLE) && rb != null)
        {
            rb.constraints =
                RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;

            stonePuzzle.quset1_Counter += 1;

            GetComponent<Collider2D>().enabled = false;
            collision.gameObject.SetActive(false);
        }
    }
}