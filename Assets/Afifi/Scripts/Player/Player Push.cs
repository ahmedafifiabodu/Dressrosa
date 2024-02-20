using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private float pushPower = 2.0f;
    [SerializeField] private LayerMask pushableLayer;
    private Vector2 lastVelocity;

    private void Update() => lastVelocity = playerRigidbody.velocity;

    private void OnCollisionStay2D(Collision2D collision)
    {
        //bitwise AND Operator
        if ((pushableLayer.value & 1 << collision.gameObject.layer) == 0)
            return;

        if (!collision.gameObject.TryGetComponent<Rigidbody2D>(out var body))
            return;

        if (lastVelocity.x != 0 && lastVelocity.y != 0)
            return;

        Vector2 pushDir;

        if (Mathf.Abs(lastVelocity.x) > Mathf.Abs(lastVelocity.y))
            pushDir = new Vector2(Mathf.Sign(lastVelocity.x), 0);
        else
            pushDir = new Vector2(0, Mathf.Sign(lastVelocity.y));

        body.AddForce(pushPower * pushDir, ForceMode2D.Force);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((pushableLayer.value & 1 << collision.gameObject.layer) == 0)
            return;

        if (!collision.gameObject.TryGetComponent<Rigidbody2D>(out var body))
            return;

        body.velocity = Vector2.zero;
    }
}