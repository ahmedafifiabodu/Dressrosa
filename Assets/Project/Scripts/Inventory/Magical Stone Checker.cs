using UnityEngine;

public class MagicalStoneChecker : MonoBehaviour
{
    [SerializeField] private GameObject _stoneCanvas;
    public Cutscene scene;


    public bool AreAllSlotsFilled()
    {
        // Get all Slot components in the inventory
        Slot[] slots = _stoneCanvas.GetComponentsInChildren<Slot>();

        // Iterate over all slots
        foreach (Slot slot in slots)
        {
            // If a slot does not have any children, it is not holding an item, so return false
            if (slot.transform.childCount == 0)
                return false;
        }

        // If we've checked all slots and none of them are empty, return true
        return true;
    }

    // For Testing
    private void Update()
    {
        if (AreAllSlotsFilled())
            scene.StartCutscene();
    }
}