using UnityEngine;

public class PlayerIsometricMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _crippledSpeed = 0.5f;
    [SerializeField] TimeTravel_System travelEffecrt;
    [SerializeField] private Animator _animator;

    private Vector2 lastMoveDirection;
    private bool facingLeft = false;
    private PlayerInformation _playerInformation;
    private float _timeSinceLastMove = 0;

    private float isoMoveX;
    private float isoMoveY;

    private void Start()
    {
        _playerInformation = PlayerInformation.Instance;
    }

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
        isoMoveX = (moveX - moveY) / 2;
        isoMoveY = (moveX + moveY) / 2;

        Vector2 isoInput = new(isoMoveX, isoMoveY);

        if (isoMoveX != 0 || isoMoveY != 0)
        {
            lastMoveDirection = isoInput;
        }

        if (travelEffecrt.effectActivated)
        {
            //isoInput *= _crippledSpeed;
            _rb.drag += Mathf.Abs(isoInput.x) * Time.deltaTime * _crippledSpeed;
            _rb.drag += Mathf.Abs(isoInput.y) * Time.deltaTime * _crippledSpeed;
            _animator.speed = _crippledSpeed;
            if (_playerInformation.IsOutOfStamina)
            {
                _rb.drag = 0;
                travelEffecrt.effectActivated = false;
            }
        }
        else
        {
            _animator.speed = 1f;
        }

        _rb.velocity = isoInput * _speed;
        Animate(isoInput);
        stamina();

        //if (_input.x < 0 && !facingLeft || _input.x > 0 && facingLeft)
        //{
        //    Flip();
        //}
    }

    private void stamina()
    {
        if (travelEffecrt.effectActivated && (isoMoveX != 0 || isoMoveY != 0))
        {
            _playerInformation.DecreaseStamina(Time.fixedDeltaTime);
        }
        else if(!travelEffecrt.effectActivated)
        {
            _playerInformation.RechargeStamina(Time.fixedDeltaTime);
        }
    }

    private void Animate(Vector2 _input)
    {
        _animator.SetFloat(GameConstant.MOVEX, _input.x);
        _animator.SetFloat(GameConstant.MOVEY, _input.y);

        _animator.SetFloat(GameConstant.MOVEMAGNITUDE, _input.magnitude);

        _animator.SetFloat(GameConstant.LASTMOVEX, lastMoveDirection.x);
        _animator.SetFloat(GameConstant.LASTMOVEY, lastMoveDirection.y);
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