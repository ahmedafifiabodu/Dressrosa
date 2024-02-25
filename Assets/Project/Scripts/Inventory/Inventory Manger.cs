using UnityEngine;
using UnityEngine.UI;

public class InventoryManger : MonoBehaviour
{
    [SerializeField] internal InputManager _inputManager;

    [SerializeField] internal Canvas _mainCanvas;
    [SerializeField] internal GameObject _inventoryCanvas;
    [SerializeField] internal GameObject _itemSlot;

    public static InventoryManger Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    internal void SetActiveForInventoryPanel(bool _setActive) => _inventoryCanvas.SetActive(_setActive);

    public void AddItemToInventory(InventoryParameters _itemParameters, Slot slot)
    {
        // Instantiate a new item slot
        GameObject newItemSlot = Instantiate(_itemSlot, slot.transform);

        Image newItemSlotImage = newItemSlot.GetComponent<Image>();
        newItemSlotImage.sprite = _itemParameters._itemSpriteRenderer.sprite;
        newItemSlotImage.type = Image.Type.Simple;

        InventoryDragAndDrop newItemSlotDragandDrop = newItemSlot.GetComponent<InventoryDragAndDrop>();
        newItemSlotDragandDrop.initPos = newItemSlot.transform.position;
        newItemSlotDragandDrop.id = _itemParameters._slotId;
        newItemSlotDragandDrop.myCanvas = _mainCanvas;

        // Set the parent of the new item slot to the slot
        newItemSlot.transform.SetParent(slot.transform);

        // Set the local position of the new item slot to (0, 0, 0) relative to its parent
        newItemSlot.transform.localPosition = Vector3.zero;

        // Set the size of the new item to match the slot size
        RectTransform newItemSlotRectTransform = newItemSlot.GetComponent<RectTransform>();
        RectTransform slotRectTransform = slot.GetComponent<RectTransform>();
        newItemSlotRectTransform.sizeDelta = slotRectTransform.sizeDelta;
    }
}