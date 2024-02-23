using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitTime = 2f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private int currentPointIndex = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void startMovement()
    {
        if (points.Length > 0)
        {
            transform.position = points[0].position;
            StartCoroutine(MoveToNextPoint());
        }
    }

    private IEnumerator MoveToNextPoint()
    {
        while (true)
        {
            var targetPoint = points[currentPointIndex].position;
            while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
            {
                animator.SetBool(GameConstant.ISWALKING, true);
                animator.SetBool(GameConstant.ISIDLE, false);

                // Flip the sprite based on the direction of movement
                if (targetPoint.x < transform.position.x)
                    spriteRenderer.flipX = false; // Moving to the right
                else if (targetPoint.x > transform.position.x)
                    spriteRenderer.flipX = true; // Moving to the left

                transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                yield return null;
            }

            animator.SetBool(GameConstant.ISWALKING, false);
            animator.SetBool(GameConstant.ISIDLE, true);

            yield return new WaitForSeconds(waitTime);

            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }
}