using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTravelSystem : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private List<GameObject> baseWorld;
    [SerializeField] private List<GameObject> reverseWorld;

    private PlayerInformation _playerInformation;
    private AudioManager _audioManager;
    private DistanceShader _travelEffect;
    private bool travel;

    internal bool canTravel;
    internal bool effectActivated;
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
        travel = false;
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

        if (effectActivated)
        {
            StartCoroutine(waitBeforeTravel(0.2f));
            _audioManager.SFXSource.Stop();
            _audioManager.SFXSource.pitch = 2;
            _audioManager.PlaySFX(_audioManager.enterTimeTravel);
        }
        else
        {
            StartCoroutine(waitBeforeTravel(1.3f));
            _audioManager.SFXSource.Stop();
            _audioManager.SFXSource.pitch = 2;
            _audioManager.PlaySFX(_audioManager.exitTimeTravel);
        }
    }

    // Update is called once per frame
    private void Update() => TravelEffect();

    private void TravelEffect()
    {
        staminaBar.value = _playerInformation._energy;
        if (effectActivated)
        {
            staminaBar.gameObject.SetActive(true);

            if (travel == true)
            {
                for (int i = 0; i < reverseWorld.Count; i++)
                    reverseWorld[i].SetActive(true);

                for (int i = 0; i < baseWorld.Count; i++)
                    baseWorld[i].SetActive(false);
            }

            _travelEffect.playerPosition = player.position;
            _travelEffect.distanceValue = Mathf.SmoothDamp(_travelEffect.distanceValue, 1, ref velocity, smoothTime);
        }
        else
        {
            if (travel == false)
            {
                for (int i = 0; i < reverseWorld.Count; i++)
                    reverseWorld[i].SetActive(false);

                for (int i = 0; i < baseWorld.Count; i++)
                    baseWorld[i].SetActive(true);
            }

            _travelEffect.playerPosition = player.position;
            _travelEffect.distanceValue = Mathf.SmoothDamp(_travelEffect.distanceValue, 0, ref velocity, smoothTime);

            if (_travelEffect.distanceValue < 0.9f)
                staminaBar.gameObject.SetActive(false);
        }

    }

    IEnumerator waitBeforeTravel(float time)
    {
        yield return new WaitForSeconds(time);
        travel = !travel;
    }
}