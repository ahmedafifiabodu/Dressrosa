using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
	public static DialogUI Instance; // Singleton instance

	public TMP_Text DialogBoxText;
	public Button NextButton;
	public float TypeTextDelay = 0.05f;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public void ShowText(string text, bool shouldType)
	{
		// Activate the dialog panel
		gameObject.SetActive(true);

		if (shouldType)
		{
			StartCoroutine(TypeText(text));
		}
		else
		{
			DialogBoxText.text = text;
		}
	}

	// Typing effect
	private IEnumerator TypeText(string text)
	{
		string fullText = text;
		string currentText;
		for (int i = 0; i < fullText.Length + 1; i++)
		{
			currentText = fullText.Substring(0, i);
			DialogBoxText.text = currentText;
			yield return new WaitForSeconds(TypeTextDelay);
		}
	}
}
