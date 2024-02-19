using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceShader : MonoBehaviour
{
    [SerializeField] private float distanceValue;
    [SerializeField] private string distanceReferance;

    void Update()
    {
        changeDistance();
    }

    private void changeDistance()
    {
        Shader.SetGlobalFloat(distanceReferance, distanceValue);
    }
}
