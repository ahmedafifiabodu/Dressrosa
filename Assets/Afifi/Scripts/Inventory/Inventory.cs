using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly List<int> ints = new();

    public void AddItem(int id)
    {
        ints.Add(id);
        Debug.Log("Item added to inventory");
    }
}