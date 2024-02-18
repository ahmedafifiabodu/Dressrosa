using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Animator _animator;

    private InputSystem _playerInput;
    private InputSystem.PlayerActions _playerActions;

    private Vector2 lastMoveDirection;
    private bool facingLeft = false;

    private void Awake()
    {
        _playerInput = new InputSystem();

        _playerActions = _playerInput.Player;

        _playerActions.Fire.performed += _ => Fire();

        //_onFoot.Switch.performed += ctx =>
        //{
        //    var scroll = ctx.ReadValue<float>();

        //    if (scroll > 0)
        //        _playerShooting.SwitchToNextWeapon();
        //    else if (scroll < 0)
        //        _playerShooting.SwitchToPreviousWeapon();
        //};
    }

    private void FixedUpdate() => ProcessMove(_playerActions.Movement.ReadValue<Vector2>());

    private void OnEnable() => _playerActions.Enable();

    private void OnDisable() => _playerActions.Disable();

    internal void ProcessMove(Vector2 _input)
    {
        float moveX = _input.x;
        float moveY = _input.y;

        if (moveX != 0 || moveY != 0)
            lastMoveDirection = _input;

        _rb.velocity = _input * _speed;
        Animate(_input);

        //if (_input.x < 0 && !facingLeft || _input.x > 0 && facingLeft)
        //{
        //    Flip();
        //}
    }

    private void Animate(Vector2 _input)
    {
        _animator.SetFloat("Move X", _input.x);
        _animator.SetFloat("Move Y", _input.y);

        _animator.SetFloat("Move Magnitude", _input.magnitude);

        _animator.SetFloat("Last Move X", lastMoveDirection.x);
        _animator.SetFloat("Last Move Y", lastMoveDirection.y);
    }

    private void Flip()
    {
        Vector3 _scale = transform.localScale;
        _scale.x *= -1;
        transform.localScale = _scale;
        facingLeft = !facingLeft;
    }

    internal void Fire()
    {
        Debug.Log("Fire");
    }
}