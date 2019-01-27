using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * CLASS SacredScroll : MonoBehaviour
 * ----------------------------------
 * Describes the sacred scroll.  Constains strings for each verse in
 * the scroll and the ability to fade the verses in and out
 * ----------------------------------
 */ 

public class LoreDisplay : MonoBehaviour 
{
	[SerializeField]
	private List<string> verses;	// Strings displayed as verses in the sacred scroll
	[SerializeField]
	private Text textBox;	// Holds the text of the scroll
	private FadingElement textFader;	// Fades the text in and out

	public Text TextBox { get { return textBox; } }

	void Start ()
	{
		textFader = textBox.GetComponent <FadingElement> ();
	}

	// Set the verse text to the one specified, and fade it in or out
	public void FadeVerse (bool active, int verse = 1)
	{
		if (active && verses [verse - 1] != null) {
			textBox.text = verses [verse - 1];
		}
		textFader.Fade (active);
	}
}