using UnityEngine;

public class MagicalStoneChecker : MonoBehaviour
{
    [SerializeField] private GameObject _stoneCanvas;
    [SerializeField] private Cutscene scene;
    [SerializeField] private Cutscene creditsCutscene;

    private bool firstCutScenePlayed = false;
    private bool secondCutScenePlayed = false;

    private bool AreAllSlotsFilled()
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

        return true;
    }

    private void Update()
    {
        if (AreAllSlotsFilled() && !firstCutScenePlayed)
        {
            scene.StartCutscene();
            firstCutScenePlayed = true;
        }
        else if (AreAllSlotsFilled() && !scene.IsCutscenePlaying && !secondCutScenePlayed)
        {
            creditsCutscene.StartCutscene();
            secondCutScenePlayed = true;
        }
    }
}