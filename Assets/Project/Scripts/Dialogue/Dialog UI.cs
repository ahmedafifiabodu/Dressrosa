using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public TMP_Text DialogBoxText;
    public Button NextButton;
    public float TypeTextDelay = 0.05f;

    public void ShowText(string text, bool shouldType)
    {
        gameObject.SetActive(true);

        if (shouldType)
            StartCoroutine(TypeText(text));
        else
            DialogBoxText.text = text;
    }

    private IEnumerator TypeText(string text)
    {
        string fullText = text;
        string currentText;
        for (int i = 0; i < fullText.Length + 1; i++)
        {
            currentText = fullText[..i];
            DialogBoxText.text = currentText;
            yield return new WaitForSeconds(TypeTextDelay);
        }
    }
}