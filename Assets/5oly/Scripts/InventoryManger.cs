using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManger : MonoBehaviour
{
    
  public  static InventoryManger instance;
    //public void MakeChild(Transform child  )
    //{
    //    child.transform.SetParent(transform);
    //}
    private void Awake()
    {
        instance = this;
    }
}
