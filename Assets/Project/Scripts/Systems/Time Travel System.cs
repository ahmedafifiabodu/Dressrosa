using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTravel_System : MonoBehaviour
{
    public static TimeTravel_System instance;
    private DistanceShader _travelEffect;
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime;
    [SerializeField] private List<GameObject> baseWorld;
    [SerializeField] private List<GameObject> reverseWorld;

    public Slider staminaBar;
    public bool canTravel;
    public bool effectActivated;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        canTravel = false;
        effectActivated = false;
        staminaBar.maxValue = PlayerInformation.Instance._stamina;
        _travelEffect = GetComponent<DistanceShader>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space) && canTravel == true)
        //{
        //    effectActivated = !effectActivated;
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            effectActivated = !effectActivated;
        }

        TravelEffect();
    }

    private void TravelEffect()
    {
        staminaBar.value = PlayerInformation.Instance._stamina;
        if(effectActivated)
        {
            staminaBar.gameObject.SetActive(true);
            for(int i = 0; i < reverseWorld.Count; i++)
            {
                reverseWorld[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < baseWorld.Count; i++)
            {
                baseWorld[i].gameObject.SetActive(false);
            }

            _travelEffect.playerPosition = player.position;
            _travelEffect.distanceValue = Mathf.SmoothDamp(_travelEffect.distanceValue, 1, ref velocity, smoothTime);
        }
        else
        {
            for (int i = 0; i < reverseWorld.Count; i++)
            {
                reverseWorld[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < baseWorld.Count; i++)
            {
                baseWorld[i].gameObject.SetActive(true);
            }
            _travelEffect.playerPosition = player.position;
            _travelEffect.distanceValue = Mathf.SmoothDamp(_travelEffect.distanceValue, 0, ref velocity, smoothTime);
            if(_travelEffect.distanceValue < 0.9f)
            {
                staminaBar.gameObject.SetActive(false);
            }
        }
    }
}
