using UnityEngine;

public class StartCutScene : MonoBehaviour
{
    private Cutscene _cutScene;
    private Collider2D _collider;

    private void Start()
    {
        _cutScene = GetComponent<Cutscene>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameConstant.PLAYERTAG))
        {
            _cutScene.StartCutscene();
            _collider.enabled = false;
        }
    }
}