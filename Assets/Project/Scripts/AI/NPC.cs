using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Movement Points (Loopable)")]
    [SerializeField] private Transform[] points;

    [Header("Target Positions (Not Loopable)")]
    [SerializeField] private Transform[] _targetPositions;

    [SerializeField] private ActivatableObject[] objectsToActivate;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 1f;

    [SerializeField] private float waitTime = 2f;

    private TimeTravelSystem _timeTravelSystem;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _moveToPointCoroutine;

    private int currentPointIndex = 0;
    private int currentTargetIndex = 0;
    private bool _isNPCStopMovingInTargetList = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _timeTravelSystem = TimeTravelSystem.Instance;

        StartMovement();
    }

    public void StartMovement()
    {
        if (points.Length > 0)
        {
            transform.position = points[0].position;
            _moveToPointCoroutine = StartCoroutine(MoveToNextPoint());
        }
    }

    private IEnumerator MoveToNextPoint()
    {
        while (true)
        {
            var targetPoint = points[currentPointIndex].position;
            while (Vector2.Distance(transform.position, targetPoint) > 0.01f)
            {
                _animator.SetBool(GameConstant.ISWALKING, true);
                _animator.SetBool(GameConstant.ISIDLE, false);

                // Flip the sprite based on the direction of movement
                if (targetPoint.x < transform.position.x)
                    _spriteRenderer.flipX = false;
                else if (targetPoint.x > transform.position.x)
                    _spriteRenderer.flipX = true;

                transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                yield return null;
            }

            _animator.SetBool(GameConstant.ISWALKING, false);
            _animator.SetBool(GameConstant.ISIDLE, true);

            yield return new WaitForSeconds(waitTime);

            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }

    public void GoToTargetPoint()
    {
        if (_moveToPointCoroutine != null)
        {
            StopCoroutine(_moveToPointCoroutine);
            _moveToPointCoroutine = null;
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
                _animator.SetBool(GameConstant.ISWALKING, true);
                _animator.SetBool(GameConstant.ISIDLE, false);

                // Flip the sprite based on the direction of movement
                if (targetPoint.x < transform.position.x)
                    _spriteRenderer.flipX = false;
                else if (targetPoint.x > transform.position.x)
                    _spriteRenderer.flipX = true;

                transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                yield return null;
            }

            _animator.SetBool(GameConstant.ISWALKING, false);
            _animator.SetBool(GameConstant.ISIDLE, true);

            // Move to the next target position
            currentTargetIndex++;

            yield return null;
        }

        // Reset the currentTargetIndex and stop moving to target point
        currentTargetIndex = 0;
        _isNPCStopMovingInTargetList = true;
    }

    internal void IsTimeTravelActive()
    {
        if (_timeTravelSystem.effectActivated && _isNPCStopMovingInTargetList)
        {
            if (objectsToActivate.Length > 0)
                foreach (var obj in objectsToActivate)
                    if (obj.isTriggeredByTimeTravel)
                        obj.gameObject.SetActive(true);
                    else if (!obj.isTriggeredByTimeTravel)
                        obj.gameObject.SetActive(true);
        }
        else if (!_timeTravelSystem.effectActivated)
        {
            if (objectsToActivate.Length > 0)
                foreach (var obj in objectsToActivate)
                    if (obj.isTriggeredByTimeTravel)
                        obj.gameObject.SetActive(false);
                    else if (!obj.isTriggeredByTimeTravel)
                        obj.gameObject.SetActive(true);
        }
    }

    private void Update() => IsTimeTravelActive();
}

[System.Serializable]
public class ActivatableObject
{
    public GameObject gameObject;
    public bool isTriggeredByTimeTravel;
}