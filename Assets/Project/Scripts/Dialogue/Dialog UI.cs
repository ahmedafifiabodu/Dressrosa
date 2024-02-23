using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
	[SerializeField] private TMP_Text DialogBoxText;
	[SerializeField] internal Button NextButton;
	[SerializeField] private float TypeTextDelay = 0.05f;
	[SerializeField] private Image panelImage;

	private bool isTyping = false;
	private string originalText = "";

	public void ChangeImage(Texture2D newTexture)
	{
		if (panelImage != null)
		{
			Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.zero);
			panelImage.sprite = newSprite;
		}
		else
		{
			Debug.LogError("Panel Image reference not set!");
		}
	}

	public void ShowText(DialogItems dialogItem, bool shouldType)
	{
		gameObject.SetActive(true);
		StopAllCoroutines(); // Stop any ongoing typing
		if (shouldType)
		{
			AudioManager.Instance.PlayDialog(dialogItem.Sound);
			if (dialogItem.LeftWrite)
				DialogBoxText.alignment = TextAlignmentOptions.Left;
			else
				DialogBoxText.alignment = TextAlignmentOptions.Right;
			ChangeImage(dialogItem.Panel);

			originalText = dialogItem.DialogText; // Store the original text
			StartCoroutine(TypeText(dialogItem.DialogText));
		}
		else
		{
			DialogBoxText.text = dialogItem.DialogText;
		}
	}

	private IEnumerator TypeText(string text)
	{
		isTyping = true;
		DialogBoxText.text = ""; // Clear text initially
		for (int i = 0; i < text.Length; i++)
		{
			if (!isTyping)
				break; // Break the loop if typing is interrupted
			DialogBoxText.text += text[i];
			yield return new WaitForSeconds(TypeTextDelay);
		}
		isTyping = false; // Typing finished
	}

	public void SkipTyping()
	{
		if (isTyping)
		{
			StopCoroutine("TypeText");
			DialogBoxText.text = originalText; // Restore the original text
			isTyping = false;
		}
	}
}
