using UnityEngine;

public class PlayerIsometricMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] internal bool _invertDirection = false;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _crippledSpeed = 0.5f;
    [SerializeField] private TimeTravelSystem travelEffecrt;
    [SerializeField] private Animator _animator;

    private PlayerInformation _playerInformation;
    private DialogManager _dialogManager;
    private AudioManager _audioManager;

    private Vector2 lastMoveDirection;
    private float isoMoveX;
    private float isoMoveY;
    private float previousDrag = 0f;
    private float animatorSpeed = 0;

    public static PlayerIsometricMovement Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _playerInformation = PlayerInformation.Instance;
        _dialogManager = DialogManager.Instance;
        _audioManager = AudioManager.Instance;
    }

    internal void ProcessMove(Vector2 _input)
    {
        // Ignore movement input if a dialog is active
        if (_dialogManager.IsDialogActive)
        {
            _rb.velocity = Vector2.zero;

            Animate(Vector2.zero);

            return;
        }

        if (_invertDirection)
            _input = -_input;

        float moveX = _input.x;
        float moveY = _input.y;

        // Adjust for isometric movement
        //isoMoveX = (moveX - moveY) / 2;
        //isoMoveY = (moveX + moveY) / 2;

        isoMoveX = moveX;
        isoMoveY = moveY;

        Vector2 isoInput = new(isoMoveX, isoMoveY);

        if (isoMoveX == 0 && isoMoveY == 0)
            _audioManager.StopWalkSFX();

        if (isoMoveX != 0 || isoMoveY != 0)
            lastMoveDirection = isoInput;

        if (travelEffecrt.effectActivated)
        {
            //isoInput *= _crippledSpeed;

            if (_rb.drag == 0 && !_playerInformation._isEnergyRecharging)
                _rb.drag = previousDrag;

            _rb.drag += Mathf.Abs(isoInput.x) * Time.deltaTime * _crippledSpeed;
            _rb.drag += Mathf.Abs(isoInput.y) * Time.deltaTime * _crippledSpeed;

            animatorSpeed = 1 - (_rb.drag / 20);
            animatorSpeed = Mathf.Clamp(animatorSpeed, 0.1f, 2f);

            _animator.speed = animatorSpeed;

            if (_playerInformation.IsOutOfStamina)
            {
                _rb.drag = 0;
                travelEffecrt.effectActivated = false;
            }
        }
        else
        {
            previousDrag = _rb.drag;
            _rb.drag = 0;
            _animator.speed = 1.5f;
        }

        _rb.velocity = isoInput * _speed;
        Animate(isoInput);
        Stamina();
    }

    private void Stamina()
    {
        if (travelEffecrt.effectActivated && (isoMoveX != 0 || isoMoveY != 0))
            _playerInformation.DecreaseStamina(Time.fixedDeltaTime);
        else if (!travelEffecrt.effectActivated)
            _playerInformation.RechargeStamina(Time.fixedDeltaTime);
    }

    private void Animate(Vector2 _input)
    {
        _animator.SetFloat(GameConstant.MOVEX, _input.x);
        _animator.SetFloat(GameConstant.MOVEY, _input.y);

        _animator.SetFloat(GameConstant.MOVEMAGNITUDE, _input.magnitude);

        _animator.SetFloat(GameConstant.LASTMOVEX, lastMoveDirection.x);
        _animator.SetFloat(GameConstant.LASTMOVEY, lastMoveDirection.y);
    }

    internal void Fire()
    {
    }
}