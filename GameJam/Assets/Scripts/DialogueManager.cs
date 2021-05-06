
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
	public TextMeshProUGUI dialogueText;

	private Queue<string> sentences;

	public int textTimer = 1000;

	[TextArea(3, 10)]
	public string[] text;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();

		for (int i = 0; i < text.Length; i++) {
			sentences.Enqueue(text[i]);
		}

		DisplayNextSentence();
	}

    private void Update()
    {
		textTimer--;
		if (textTimer <= 0) {
			DisplayNextSentence();
			textTimer = 1000;
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
			yield return null;
		}
	}

	private void EndDialogue()
	{

	}

}