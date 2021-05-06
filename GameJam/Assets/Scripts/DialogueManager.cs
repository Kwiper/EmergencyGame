
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public TextMeshProUGUI dialogueText;

	public float typingSpeed;

	private Queue<string> sentences;

	public int textTimer = 5000;
	private int textCount;

	[TextArea(3, 10)]
	public string[] text;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();

		for (int i = 0; i < text.Length; i++) {
			sentences.Enqueue(text[i]);
		}
		textCount = textTimer;
		DisplayNextSentence();
	}

    private void Update()
    {
		textCount--;
		if (textCount <= 0) {
			DisplayNextSentence();
			textCount = textTimer;
		}
    }


    public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSecondsRealtime(typingSpeed);
		}
	}

	private void EndDialogue()
	{

	}

}