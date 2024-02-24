using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private GameObject cutsceneParent;
    [SerializeField] private Image cutsceneBackground;
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private List<Sprite> cutsceneFrames;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private AudioClip audioClip;

    private InputManager _inputManager;
    private AudioManager _audioManager;

    private void Start()
    {
        _inputManager = InputManager.Instance;
        _audioManager = AudioManager.Instance;

        // Set the size of the image to match the screen size
        RectTransform rectTransformCutsceneImage = cutsceneImage.GetComponent<RectTransform>();
        rectTransformCutsceneImage.sizeDelta = new Vector2(Screen.width, Screen.height);

        RectTransform rectTransformBackground = cutsceneBackground.GetComponent<RectTransform>();
        rectTransformBackground.sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    public void StartCutscene()
    {
        cutsceneParent.SetActive(true);
        _inputManager._playerInput.Disable();
        _audioManager.PlaySFX(audioClip);
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        foreach (Sprite frame in cutsceneFrames)
        {
            cutsceneImage.sprite = frame;
            yield return FadeIn();
            yield return new WaitForSeconds(2); // Wait for 2 seconds before showing the next frame
            yield return FadeOut();
        }

        cutsceneParent.SetActive(false); // Deactivate the image after the cutscene
        _inputManager._playerInput.Enable(); // Enable player's input
        cutsceneParent.SetActive(false); // Deactivate the image after the cutscene
    }

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            Color color = cutsceneImage.color;
            color.a = Mathf.Lerp(0, 1, t / transitionTime);
            cutsceneImage.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 0;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            Color color = cutsceneImage.color;
            color.a = Mathf.Lerp(1, 0, t / transitionTime);
            cutsceneImage.color = color;
            yield return null;
        }
    }
}