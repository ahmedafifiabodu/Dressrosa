using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler ,IDragHandler
{
   // public Image image;
    private RectTransform rectTransform;
    public Canvas myCanvas;
    private CanvasGroup mycanvasGroup;
    public int id;
    private Vector2 initPos;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        mycanvasGroup = GetComponent<CanvasGroup>();
        initPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
         mycanvasGroup.blocksRaycasts = false;
        // image.raycastTarget = false;

        //InventoryManger.instance.MakeChild(transform);
        eventData.pointerDrag.transform.SetParent(InventoryManger.instance.transform);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("OnDrag");
        // the amount of movement we did with the mouse since the last frame while holding the game object
        rectTransform.anchoredPosition += eventData.delta /myCanvas.scaleFactor;
        
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
       mycanvasGroup.blocksRaycasts = true;
      //  image.raycastTarget = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("CLICK");
       
    }
    public void ResetPosition()
    {
        transform.position = initPos;
    }
}
