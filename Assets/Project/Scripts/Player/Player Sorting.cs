using UnityEngine;

public class PlayerSorting : MonoBehaviour
{
    private SpriteRenderer sortingSpriteRenderer;
    private readonly int sortingOrderPixelPerUnit = GameConstant.sortingOrderPixelPerUnit;

    private void Start() =>
        sortingSpriteRenderer = GetComponent<SpriteRenderer>();

    private void Update() => AdjustingSortingLayer();

    private void AdjustingSortingLayer()
    {
        //sortingSpriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        //sortingSpriteRenderer.sortingOrder = transform.position.y * -1 + sortingOrder;

        sortingSpriteRenderer.sortingOrder = (int)(transform.position.y * -sortingOrderPixelPerUnit);
    }
}