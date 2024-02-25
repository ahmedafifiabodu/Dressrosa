using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform[] _targetPositions;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitTime = 2f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Coroutine moveToPointCoroutine;

    private int currentPointIndex = 0;
    private int currentTargetIndex = 0;

    private bool moveToTargetPoint = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartMovement();
    }

    public void StartMovement()
    {
        if (points.Length > 0)
        {
            transform.position = points[0].position;
            moveToPointCoroutine = StartCoroutine(MoveToNextPoint());
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
                    spriteRenderer.flipX = false;
                else if (targetPoint.x > transform.position.x)
                    spriteRenderer.flipX = true;

                transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                yield return null;
            }

            animator.SetBool(GameConstant.ISWALKING, false);
            animator.SetBool(GameConstant.ISIDLE, true);

            yield return new WaitForSeconds(waitTime);

            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }

    public void GoToTargetPoint()
    {
        if (moveToPointCoroutine != null)
        {
            StopCoroutine(moveToPointCoroutine);
            moveToPointCoroutine = null;
        }

        StartCoroutine(MoveToTargetPoint());
    }

    private IEnumerator MoveToTargetPoint()
    {
        while (currentTargetIndex < _targetPositions.Length)
        {
            var targetPoint = new Vector3(
                _targetPositions[currentTargetIndex].position.x,
                _targetPositions[currentTargetIndex].position.y, 0);

            while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
            {
                animator.SetBool(GameConstant.ISWALKING, true);
                animator.SetBool(GameConstant.ISIDLE, false);

                // Flip the sprite based on the direction of movement
                if (targetPoint.x < transform.position.x)
                    spriteRenderer.flipX = false;
                else if (targetPoint.x > transform.position.x)
                    spriteRenderer.flipX = true;

                transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                yield return null;
            }

            animator.SetBool(GameConstant.ISWALKING, false);
            animator.SetBool(GameConstant.ISIDLE, true);

            // Move to the next target position
            currentTargetIndex++;

            yield return null;
        }

        // Reset the currentTargetIndex and stop moving to target point
        currentTargetIndex = 0;
        moveToTargetPoint = false;
    }
}