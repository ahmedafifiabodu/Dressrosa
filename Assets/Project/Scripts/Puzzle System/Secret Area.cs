using System.Collections;
using UnityEngine;

public class SecretArea : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color hiddenColor;
    private Coroutine fadeOutCoroutine;

    private void Start()
    {
        hiddenColor = spriteRenderer.color;
        spriteRenderer.color = new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.PLAYERTAG) && gameObject.activeInHierarchy)
        {
            if (fadeOutCoroutine != null)
                StopCoroutine(fadeOutCoroutine);

            fadeOutCoroutine = StartCoroutine(FadeOut(true));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameConstant.PLAYERTAG) && gameObject.activeInHierarchy)
        {
            if (fadeOutCoroutine != null)
                StopCoroutine(fadeOutCoroutine);

            fadeOutCoroutine = StartCoroutine(FadeOut(false));
        }
    }

    private void OnEnable()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(255, 255, 255, 1);
    }

    private void OnDisable()
    {
        if (fadeOutCoroutine != null)
            StopCoroutine(fadeOutCoroutine);

        if (spriteRenderer != null)
            spriteRenderer.color = new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 0);
    }

    private IEnumerator FadeOut(bool fadeOut)
    {
        Color startColor = spriteRenderer.color;
        Color targetColor = fadeOut ? hiddenColor : new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 0);

        float timer = 0;

        while (timer < fadeDuration)
        {
            spriteRenderer.color = Color.Lerp(startColor, targetColor, timer / fadeDuration);
            timer += Time.deltaTime;

            yield return null;
        }

        spriteRenderer.color = targetColor;
    }
}