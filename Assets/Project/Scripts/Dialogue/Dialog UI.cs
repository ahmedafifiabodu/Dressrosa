using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private TMP_Text DialogBoxLeftText;
    [SerializeField] private TMP_Text DialogBoxRightText;
    [SerializeField] internal Button NextButton;
    [SerializeField] private float TypeTextDelay = 0.05f;
    [SerializeField] private Image panelImage;

    private bool isTyping = false;

    private AudioManager audioManager;

    private void Awake() => audioManager = AudioManager.Instance;

    public void ChangeImage(Texture2D newTexture)
    {
        if (panelImage != null)
        {
            Sprite newSprite =
                Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);

            panelImage.sprite = newSprite;
        }
    }

    public void ShowText(DialogItems dialogItem, bool shouldType)
    {
        gameObject.SetActive(true);
        StopAllCoroutines();

        if (shouldType)
        {
            ChangeImage(dialogItem.Panel);
            audioManager.PlayDialog(dialogItem.Sound);

            ChangeImage(dialogItem.Panel);

            if (dialogItem.LeftWrite)
            {
                DialogBoxRightText.text = "";
                StartCoroutine(TypeText(dialogItem.DialogText, DialogBoxLeftText));
            }
            else
            {
                DialogBoxLeftText.text = "";
                StartCoroutine(TypeText(dialogItem.DialogText, DialogBoxRightText));
            }
        }
    }

    private IEnumerator TypeText(string text, TMP_Text tMP_Text)
    {
        isTyping = true;
        tMP_Text.text = ""; // Clear text initially

        for (int i = 0; i < text.Length; i++)
        {
            if (!isTyping)
                break;

            tMP_Text.text += text[i];
            yield return new WaitForSeconds(TypeTextDelay);
        }

        isTyping = false;
    }

    //public void SkipTyping()
    //{
    //    if (isTyping)
    //    {
    //        StopCoroutine(nameof(TypeText));
    //        DialogBoxText.text = originalText; // Restore the original text
    //        isTyping = false;
    //    }
    //}
}