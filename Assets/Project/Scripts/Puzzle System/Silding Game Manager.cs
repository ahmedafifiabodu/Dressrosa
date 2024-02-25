using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SildingGameManager : MonoBehaviour
{
    [Header("Clue System")]
    [SerializeField] private Transform gameTransform;

    [SerializeField] private Transform piecePrefab;
    [SerializeField] private GameObject puzzleTexture;
    [SerializeField] private int size = 3;
    [SerializeField] private bool activateChildren = true;

    private List<Transform> pieces;
    private int emptyLocation;
    private bool shuffling = false;
    private bool _isQuestCompleted = false;

    internal bool IsQuestCompleted() => _isQuestCompleted;

    private void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width,
                                                  +1 - (2 * width * row) - width,
                                                  0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));
                    mesh.uv = uv;
                }
            }
        }
        shuffling = true;
        StartCoroutine(WaitShuffle(0.5f));
    }

    private void Start()
    {
        pieces = new List<Transform>();
        CreateGamePieces(0.01f);
        SetChildrenActivation(activateChildren);
    }

    private void Update()
    {
        if (!shuffling && CheckCompletion())
            StartCoroutine(EnablePuzzleTextureAfterDelay(2f));

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size - 1)) { break; }
                    }
                }
            }
        }
    }

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));
            emptyLocation = i;
            return true;
        }
        return false;
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
            if (pieces[i].name != $"{i}")
                return false;

        return true;
    }

    private IEnumerator EnablePuzzleTextureAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        puzzleTexture.SetActive(true);
        _isQuestCompleted = true;

        foreach (var piece in pieces)
            piece.gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        puzzleTexture.SetActive(false);
        Destroy(gameObject);
    }

    private IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    private void Shuffle()
    {
        int count = 0;
        int last = 0;
        while (count < (size * size * size))
        {
            int rnd = Random.Range(0, size * size);
            if (rnd == last) continue;
            last = emptyLocation;

            if (SwapIfValid(rnd, -size, size))
                count++;
            else if (SwapIfValid(rnd, +size, size))
                count++;
            else if (SwapIfValid(rnd, -1, 0))
                count++;
            else if (SwapIfValid(rnd, +1, size - 1))
                count++;
        }
    }

    public void SetChildrenActivation(bool activate)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(activate);
    }
}