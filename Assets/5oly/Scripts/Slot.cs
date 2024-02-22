using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler, IDropHandler
{
    public int id;

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("item dropped");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("item dropped2");
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<DragAndDrop>().id == id)
            {
                // Set the parent of the dragged item to this slot
                eventData.pointerDrag.transform.SetParent(transform);

                // Set the local position of the item to (0, 0, 0) relative to its parent
                eventData.pointerDrag.transform.localPosition = Vector3.zero;
            }
            else
            {
                eventData.pointerDrag.GetComponent<DragAndDrop>().ResetPosition();
            }
        }
    }
}
