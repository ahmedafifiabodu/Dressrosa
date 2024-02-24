using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTravelSystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime;
    [SerializeField] private List<GameObject> baseWorld;
    [SerializeField] private List<GameObject> reverseWorld;
    [SerializeField] private Slider staminaBar;

    internal bool canTravel;
    internal bool effectActivated;

    private PlayerInformation _playerInformation;
    private AudioManager _audioManager;
    private DistanceShader _travelEffect;
    private float velocity;

    public static TimeTravelSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        canTravel = false;
        effectActivated = false;

        _playerInformation = PlayerInformation.Instance;
        _audioManager = AudioManager.Instance;
        _travelEffect = GetComponent<DistanceShader>();

        staminaBar.maxValue = _playerInformation._energy;
    }

    internal void ActiveTimeTravel()
    {
        effectActivated = !effectActivated;
        _audioManager.PlaySFX(_audioManager.timetravel);
    }

    // Update is called once per frame
    private void Update() => TravelEffect();

    private void TravelEffect()
    {
        staminaBar.value = _playerInformation._energy;

        if (effectActivated)
        {
            staminaBar.gameObject.SetActive(true);
            for (int i = 0; i < reverseWorld.Count; i++)
                reverseWorld[i].SetActive(true);

            for (int i = 0; i < baseWorld.Count; i++)
                baseWorld[i].SetActive(false);

            _travelEffect.playerPosition = player.position;
            _travelEffect.distanceValue = Mathf.SmoothDamp(_travelEffect.distanceValue, 1, ref velocity, smoothTime);
        }
        else
        {
            for (int i = 0; i < reverseWorld.Count; i++)
                reverseWorld[i].SetActive(false);

            for (int i = 0; i < baseWorld.Count; i++)
                baseWorld[i].SetActive(true);

            _travelEffect.playerPosition = player.position;
            _travelEffect.distanceValue = Mathf.SmoothDamp(_travelEffect.distanceValue, 0, ref velocity, smoothTime);

            if (_travelEffect.distanceValue < 0.9f)
                staminaBar.gameObject.SetActive(false);
        }
    }
}