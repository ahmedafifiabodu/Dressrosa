using UnityEngine;

public class AdjustingSortingLayer : MonoBehaviour
{
    private SpriteRenderer sortingSpriteRenderer;
    private readonly int sortingOrderPixelPerUnit = GameConstant.sortingOrderPixelPerUnit;

    private void Start()
    {
        sortingSpriteRenderer = GetComponent<SpriteRenderer>();
        sortingSpriteRenderer.sortingOrder = (int)(transform.position.y * -sortingOrderPixelPerUnit);
    }
}