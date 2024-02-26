using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] internal float _energy = 10;

    internal bool _isEnergyRecharging = false;
    private float _maxEnergy;

    public static PlayerInformation Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() => _maxEnergy = _energy;

    internal bool IsOutOfStamina => _energy <= 0;

    internal void DecreaseStamina(float amount)
    {
        _energy -= amount;
        if (_energy < 0)
            _energy = 0;
    }

    internal void RechargeStamina(float amount)
    {
        _energy += amount;
        _isEnergyRecharging = true;

        if (_energy > _maxEnergy)
        {
            _energy = _maxEnergy;
            _isEnergyRecharging = false;
        }
    }
}