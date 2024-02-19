using UnityEngine;

public class PlayerIsometricMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    [Range(0, 0.9f)][SerializeField] private float _crippledSpeed = 0.5f;
    [SerializeField] private Animator _animator;

    private InputSystem _playerInput;
    private InputSystem.PlayerActions _playerActions;

    private Vector2 lastMoveDirection;
    private bool facingLeft = false;
    private PlayerInformation _playerInformation;
    private float _timeSinceLastMove = 0;

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

    private void Start() => _playerInformation = PlayerInformation.Instance;

    private void FixedUpdate() => ProcessMove(_playerActions.Movement.ReadValue<Vector2>());

    private void OnEnable() => _playerActions.Enable();

    private void OnDisable() => _playerActions.Disable();

    internal void ProcessMove(Vector2 _input)
    {
        //To Make the player to stop from movement when out of stamina
        /*        if (_playerInformation.IsOutOfStamina)
                {
                    _input = Vector2.zero;
                }
                else
                {
                    float moveX = _input.x;
                    float moveY = _input.y;

                    if (moveX != 0 || moveY != 0)
                    {
                        lastMoveDirection = _input;
                        _playerInformation.DecreaseStamina(Time.fixedDeltaTime);
                    }
                }*/

        float moveX = _input.x;
        float moveY = _input.y;

        // Adjust for isometric movement
        float isoMoveX = (moveX - moveY) / 2;
        float isoMoveY = (moveX + moveY) / 2;

        Vector2 isoInput = new Vector2(isoMoveX, isoMoveY);

        if (isoMoveX != 0 || isoMoveY != 0)
        {
            lastMoveDirection = isoInput;
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
            isoInput *= 0.5f;
            _animator.speed = 0.5f;
        }
        else
        {
            _animator.speed = 1f;
        }

        _rb.velocity = isoInput * _speed;
        Animate(isoInput);

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