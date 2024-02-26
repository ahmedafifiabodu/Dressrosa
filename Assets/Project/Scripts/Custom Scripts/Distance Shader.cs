using UnityEngine;

public class DistanceShader : MonoBehaviour
{
    public float distanceValue;
    public Vector2 playerPosition;
    [SerializeField] private string distanceReferance;
    [SerializeField] private string playerPos;

    private void Start()
    {
        distanceValue = 0;
        playerPosition = Vector2.zero;
    }

    private void Update()
    {
        changeDistance();
    }

    private void changeDistance()
    {
        Shader.SetGlobalFloat(distanceReferance, distanceValue);
        Shader.SetGlobalVector(playerPos, playerPosition);
    }
}