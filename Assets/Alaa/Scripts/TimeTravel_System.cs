using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTravel_System : MonoBehaviour
{
    private DistanceShader _travelEffect;
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime;
    public bool effectActivated;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        effectActivated = false;
        _travelEffect = GetComponent<DistanceShader>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            effectActivated = !effectActivated;
        }

        TravelEffect();
    }

    private void TravelEffect()
    {
        if(effectActivated)
        {
            _travelEffect.playerPosition = player.position;
            _travelEffect.distanceValue = Mathf.SmoothDamp(_travelEffect.distanceValue, 1, ref velocity, smoothTime);
        }
        else
        {
            _travelEffect.playerPosition = player.position;
            _travelEffect.distanceValue = Mathf.SmoothDamp(_travelEffect.distanceValue, 0, ref velocity, smoothTime);
        }
    }
}
