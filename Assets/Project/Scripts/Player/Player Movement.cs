using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    [Range(0, 0.9f)][SerializeField] private float _crippledSpeed = 0.5f;
    [SerializeField] private Animator _animator;

    private Vector2 lastMoveDirection;
    private float _timeSinceLastMove = 0;
    private PlayerInformation _playerInformation;

    private void Awake() => _playerInformation = PlayerInformation.Instance;

    internal void ProcessMove(Vector2 _input)
    {
        float moveX = _input.x;
        float moveY = _input.y;

        if (moveX != 0 || moveY != 0)
        {
            lastMoveDirection = _input;
            _playerInformation.DecreaseStamina(Time.fixedDeltaTime);
            _timeSinceLastMove = 0;
        }
        else
        {
            _timeSinceLastMove += Time.fixedDeltaTime;
            if (_timeSinceLastMove >= 0.5f)
            {
                _playerInformation.RechargeStamina(Time.fixedDeltaTime);
            }
        }

        if (_playerInformation.IsOutOfStamina)
        {
            _input *= 0.5f;
            _animator.speed = 0.5f;
        }
        else
        {
            _animator.speed = 1f;
        }

        _rb.velocity = _input * _speed;
        Animate(_input);

        /*        if (_input.x < 0 && !facingLeft || _input.x > 0 && facingLeft)
                {
                    Flip();
                }*/
    }

    private void Animate(Vector2 _input)
    {
        _animator.SetFloat("Move X", _input.x);
        _animator.SetFloat("Move Y", _input.y);

        _animator.SetFloat("Move Magnitude", _input.magnitude);

        _animator.SetFloat("Last Move X", lastMoveDirection.x);
        _animator.SetFloat("Last Move Y", lastMoveDirection.y);
    }

    internal void Fire()
    {
    }
}