using System.Collections;
using UnityEngine;

public class SecretArea : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;

    private SpriteRenderer spriteRenderer;
    private Color hiddenColor;
    private Coroutine fadeOutCoroutine;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        hiddenColor = spriteRenderer.color;
        spriteRenderer.color = new Color(hiddenColor.r, hiddenColor.g, hiddenColor.b, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }
            fadeOutCoroutine = StartCoroutine(FadeOut(true));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }
            fadeOutCoroutine = StartCoroutine(FadeOut(false));
        }
    }

    private IEnumerator FadeOut(bool fadeOut)
    {
        Color startColor = spriteRenderer.color;
        // Swap the target colors
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