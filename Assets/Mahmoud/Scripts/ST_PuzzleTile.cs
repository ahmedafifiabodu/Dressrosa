using UnityEngine;

public class ST_PuzzleTile : MonoBehaviour
{
	public Vector2Int GridLocation { get; set; }
	public bool CorrectLocation { get; private set; }

	public void SetCorrectLocation(bool isCorrect)
	{
		CorrectLocation = isCorrect;
	}

	public void MoveTo(Vector2Int newLocation)
	{
		GridLocation = newLocation;
		transform.position = new Vector3(GridLocation.x, GridLocation.y, 0);
	}
}