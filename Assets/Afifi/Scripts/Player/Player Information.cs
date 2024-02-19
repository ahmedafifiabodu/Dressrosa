using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private float _stamina = 10;
    [SerializeField] private float _energy = 10;

    private float _maxStamina;

    public static PlayerInformation Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _maxStamina = _stamina;
    }

    internal bool IsOutOfStamina => _stamina <= 0;

    internal void DecreaseStamina(float amount)
    {
        _stamina -= amount;
        if (_stamina < 0)
            _stamina = 0;
    }

    internal void RechargeStamina(float amount)
    {
        _stamina += amount;
        if (_stamina > _maxStamina)
            _stamina = _maxStamina;
    }
}