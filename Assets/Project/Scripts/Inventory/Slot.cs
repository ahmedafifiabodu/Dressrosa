using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    [SerializeField] internal List<int> ids = new();

    private InventoryManger _inventoryManger;

    private void Start() => _inventoryManger = InventoryManger.Instance;

    internal bool IsOccupied { get => transform.childCount > 0; }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.TryGetComponent<DragAndDrop>(out var dragAndDropComponent))
            {
                if (ids.Contains(dragAndDropComponent.id))
                {
                    if (transform.childCount > 0)
                    {
                        Slot[] slots = _inventoryManger._inventoryCanvas.GetComponentsInChildren<Slot>();
                        Slot nextSlot = System.Array.Find(slots,
                            slot => slot.ids.Contains(dragAndDropComponent.id) && slot.transform.childCount == 0);

                        if (nextSlot == null)
                        {
                            dragAndDropComponent.ResetPosition();
                            return;
                        }

                        eventData.pointerDrag.transform.SetParent(nextSlot.transform);
                    }
                    else
                    {
                        eventData.pointerDrag.transform.SetParent(transform);
                    }

                    eventData.pointerDrag.transform.localPosition = Vector3.zero;

                    // Update initPos to the new slot's position
                    dragAndDropComponent.initPos = eventData.pointerDrag.transform.position;
                }
                else
                    dragAndDropComponent.ResetPosition();
            }
        }
    }
}