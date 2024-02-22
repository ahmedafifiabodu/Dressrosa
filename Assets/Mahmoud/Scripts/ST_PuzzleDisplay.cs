using UnityEngine;

public class ST_PuzzleDisplay : MonoBehaviour
{
	public GameObject tilePrefab;
	public Texture2D puzzleImage;
	public int width = 3;
	public int height = 3;
	public float tileSize = 1.0f;

	private ST_PuzzleTile[,] tiles;
	private bool isCompleted = false;

	void Start()
	{
		CreatePuzzle();
		ShufflePuzzle();
	}

	void CreatePuzzle()
	{
		tiles = new ST_PuzzleTile[width, height];
		Vector3 startPosition = new Vector3(-(width - 1) * tileSize / 2, (height - 1) * tileSize / 2, 0);

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				GameObject tileGO = Instantiate(tilePrefab, transform);
				ST_PuzzleTile tile = tileGO.GetComponent<ST_PuzzleTile>();
				tile.GridLocation = new Vector2Int(x, y);
				tile.SetCorrectLocation(true); // Assuming correct location at the beginning
				tile.MoveTo(new Vector2Int(x, y));

				// Set texture
				SpriteRenderer spriteRenderer = tileGO.GetComponent<SpriteRenderer>();
				if (spriteRenderer != null && puzzleImage != null)
				{
					Rect rect = new Rect(x * (1.0f / width), y * (1.0f / height), 1.0f / width, 1.0f / height);
					spriteRenderer.sprite = Sprite.Create(puzzleImage, rect, new Vector2(0.5f, 0.5f));
				}

				tiles[x, y] = tile;
			}
		}
	}

	void ShufflePuzzle()
	{
		// Shuffle by moving tiles randomly
		for (int i = 0; i < 100; i++)
		{
			int randomX = Random.Range(0, width);
			int randomY = Random.Range(0, height);
			Vector2Int randomDirection = new Vector2Int(Random.Range(-1, 2), Random.Range(-1, 2));
			TryMoveTile(tiles[randomX, randomY], randomDirection);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CheckForCompletion();
		}
	}

	void CheckForCompletion()
	{
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (tiles[x, y].GridLocation != new Vector2Int(x, y))
				{
					isCompleted = false;
					Debug.Log("Puzzle is not completed yet!");
					return;
				}
			}
		}

		isCompleted = true;
		Debug.Log("Puzzle is completed!");
	}

	public void TryMoveTile(ST_PuzzleTile tile, Vector2Int direction)
	{
		if (isCompleted)
			return;

		Vector2Int targetLocation = tile.GridLocation + direction;
		if (IsValidLocation(targetLocation))
		{
			tile.MoveTo(targetLocation);
			tiles[targetLocation.x, targetLocation.y] = tile;
			tiles[tile.GridLocation.x, tile.GridLocation.y] = null;
		}
	}

	bool IsValidLocation(Vector2Int location)
	{
		return location.x >= 0 && location.x < width &&
			   location.y >= 0 && location.y < height &&
			   tiles[location.x, location.y] == null;
	}
}