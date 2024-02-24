using UnityEngine;

public class InventoryParameters : MonoBehaviour
{
    [SerializeField] internal SpriteRenderer _itemSpriteRenderer;
    [SerializeField] internal int _slotId;

    internal InventoryManger _inventoryManger;

    private void Start() => _inventoryManger = InventoryManger.Instance;
}