using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Item ID")]
    [SerializeField] internal int id;

    [SerializeField] internal Canvas myCanvas;

    internal Vector2 initPos;

    private CanvasGroup mycanvasGroup;
    private RectTransform rectTransform;
    private InventoryManger _inventoryManger;

    private void Start()
    {
        initPos = transform.position;
        _inventoryManger = InventoryManger.Instance;

        rectTransform = GetComponent<RectTransform>();
        mycanvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        mycanvasGroup.blocksRaycasts = false;
        eventData.pointerDrag.transform.SetParent(_inventoryManger._inventoryCanvas.transform);
        initPos = transform.localPosition; // Update initPos after changing the parent
    }

    // The amount of movement we did with the mouse since the last frame while holding the game object
    public void OnDrag(PointerEventData eventData) =>
        rectTransform.anchoredPosition += eventData.delta / myCanvas.scaleFactor;

    public void OnEndDrag(PointerEventData eventData)
    {
        mycanvasGroup.blocksRaycasts = true;

        if (transform.parent == _inventoryManger._inventoryCanvas.transform)
            ResetPosition();
    }

    internal void ResetPosition() => transform.position = initPos;
}