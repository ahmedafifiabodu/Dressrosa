using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] internal List<int> ids = new();

    internal bool IsOccupied { get => transform.childCount > 0; }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent<DragAndDrop>(out var dragAndDropComponent))
            {
                // Check if the id of the dragged item is in the list of ids of this slot
                if (ids.Contains(dragAndDropComponent.id))
                {
                    // If this slot is already occupied, find the next available slot
                    if (transform.childCount > 0)
                    {
                        // Get all slots in the inventory
                        Slot[] slots = InventoryManger.Instance._inventoryCanvas.GetComponentsInChildren<Slot>();

                        // Find the first available slot (i.e., a slot that does not have a child)
                        Slot nextSlot = System.Array.Find(slots,
                            slot => slot.ids.Contains(dragAndDropComponent.id) && slot.transform.childCount == 0);

                        // If no available slot was found, reset the position of the dragged item and return
                        if (nextSlot == null)
                        {
                            dragAndDropComponent.ResetPosition();
                            return;
                        }

                        // Set the parent of the dragged item to the next available slot
                        eventData.pointerDrag.transform.SetParent(nextSlot.transform);
                    }
                    else
                    {
                        // Set the parent of the dragged item to this slot
                        eventData.pointerDrag.transform.SetParent(transform);
                    }

                    // Set the local position of the item to (0, 0, 0) relative to its parent
                    eventData.pointerDrag.transform.localPosition = Vector3.zero;
                }
                else
                    dragAndDropComponent.ResetPosition();
            }
        }
    }
}