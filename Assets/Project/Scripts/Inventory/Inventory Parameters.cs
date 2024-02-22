using UnityEngine;

public class InventoryParameters : MonoBehaviour
{
    [SerializeField] internal SpriteRenderer _itemSpriteRenderer;
    [SerializeField] internal int _slotId;

    [SerializeField] internal InventoryManger _inventoryManger;

    //private void Update()
    //{
    //    if (_inventoryManger._inputManager._playerInput.Player.Interact.triggered)
    //        _inventoryManger.AddItemToInventory(this);
    //}
}