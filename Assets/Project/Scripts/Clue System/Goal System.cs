using UnityEngine;

public class GoalSystem : MonoBehaviour
{
    private bool _isGoalCompleted;

    public bool IsGoalCompleted()
    {
        if (_isGoalCompleted)
            return true;

        return false;
    }

    public void SetGoalCompleted(bool value) => _isGoalCompleted = value;
}