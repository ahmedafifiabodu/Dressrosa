using UnityEngine;

public class AdjustingSortingLayer : MonoBehaviour
{
    private SpriteRenderer sortingSpriteRenderer;

    private void Start()
    {
        sortingSpriteRenderer = GetComponent<SpriteRenderer>();
        sortingSpriteRenderer.sortingOrder = (int)(transform.position.y * -100);
    }
}