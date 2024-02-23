using UnityEngine;

public class PlayerIsometricMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DialogManager dialogManager;

    [Header("Movement")]
    [SerializeField] internal bool _invertDirection = false;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _crippledSpeed = 0.5f;
    [SerializeField] private TimeTravelSystem travelEffecrt;
    [SerializeField] private Animator _animator;

    private Vector2 lastMoveDirection;
    private bool facingLeft = false;
    private PlayerInformation _playerInformation;
    private AudioManager _audioManager;

    private float isoMoveX;
    private float isoMoveY;

    private void Start()
    {
        _playerInformation = PlayerInformation.Instance;
        _audioManager = AudioManager.Instance;
    }

    internal void ProcessMove(Vector2 _input)
    {
        // Ignore movement input if a dialog is active
        if (dialogManager.IsDialogActive)
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
            AudioManager.Instance.StopWalkSFX();

        if (isoMoveX != 0 || isoMoveY != 0)
            lastMoveDirection = isoInput;

        if (travelEffecrt.effectActivated)
        {
            //isoInput *= _crippledSpeed;
            _rb.drag += Mathf.Abs(isoInput.x) * Time.deltaTime * _crippledSpeed;
            _rb.drag += Mathf.Abs(isoInput.y) * Time.deltaTime * _crippledSpeed;
            _animator.speed = 0.5f;
            if (_playerInformation.IsOutOfStamina)
            {
                _rb.drag = 0;
                travelEffecrt.effectActivated = false;
            }
        }
        else
            _animator.speed = 1.5f;

        _rb.velocity = isoInput * _speed;
        Animate(isoInput);
        Stamina();

        //if (_input.x < 0 && !facingLeft || _input.x > 0 && facingLeft)
        //{
        //    Flip();
        //}
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
        _animator.SetInteger(GameConstant.MOVEX, (int)_input.x);
        _animator.SetInteger(GameConstant.MOVEY, (int)_input.y);

        _animator.SetInteger(GameConstant.MOVEMAGNITUDE, (int)_input.magnitude);

        _animator.SetInteger(GameConstant.LASTMOVEX, (int)lastMoveDirection.x);
        _animator.SetInteger(GameConstant.LASTMOVEY, (int)lastMoveDirection.y);

        // Check if the player is moving isometrically
        //bool isMovingIsometrically = isoMoveX != 0 && isoMoveY != 0;
        //_animator.SetBool("isDirectional", isMovingIsometrically);
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
    }
}