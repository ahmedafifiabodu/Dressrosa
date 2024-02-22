using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
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
    }

    // The amount of movement we did with the mouse since the last frame while holding the game object
    public void OnDrag(PointerEventData eventData) =>
        rectTransform.anchoredPosition += eventData.delta / myCanvas.scaleFactor;

    public void OnEndDrag(PointerEventData eventData)
    {
        mycanvasGroup.blocksRaycasts = true;

        // Check if the current parent is still the inventory canvas
        // If it is, then the item was not dropped on a valid slot
        // So, reset its position
        if (transform.parent == _inventoryManger._inventoryCanvas.transform)
            ResetPosition();
    }

    internal void ResetPosition() => transform.position = initPos;
}