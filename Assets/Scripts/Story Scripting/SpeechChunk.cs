using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * CLASS SpeechChunk
 * -----------------
 * Describes a chunk of speech, complete with its own string for the text,
 * text box for display, speech speed, skippability, etc.
 * -----------------
 */ 
[Serializable]
public class SpeechChunk 
{
	[SerializeField]
	private string text;	// The text of the speech chunk
	[SerializeField]
	private Text textBox;	// Text element that displays this chunk of speech
	[SerializeField]
	private Speed speechSpeed;	// Speed at which this chunk of speech is said
	[SerializeField]
	private bool skippable;	// True if this chunk of speech can be skipped

	public Text TextBox { get { return textBox; } }
	public string theText { get { return text; } }
	public bool Skippable { get { return skippable; } }

	// Return precise time between each character's display,
	// based on the speed of the speech chunk
	public float timeBetweenChars {
		get {
			float tempFloat = 0.1f;	// Temporary float to be returned

			// Assign float based on the value for the speech speed
			switch (speechSpeed) {
			case Speed.Slow:
				tempFloat = 0.2f;
				break;
			case Speed.Medium:
				tempFloat = 0.1f;
				break;
			case Speed.Fast:
				tempFloat = 0.05f;
				break;
			}

			return tempFloat;
		} // END get
	} // END property

	// True if the text of the speech is fully displayed in the text box
	public bool textFullyDisplayed {
		get {
			return textBox.text == theText;
		} // END get
	} // END property

	// Trim the white space off of the text and disable the display
	public void Initialize ()
	{
		DisplayActive (false);
		theText.Trim ();
	}

	// Add a character from the text to the text box display
	public void AddChar (int index)
	{
		textBox.text += theText [index];
	}

	// Display the whole speech text
	public void DisplayFullText ()
	{
		DisplayActive (true);
		textBox.text = theText;
	}

	// Enable of dispaly the text box display
	// and clear it if it is disabled
	public void DisplayActive (bool active)
	{
		if (!active) {
			textBox.text = "";
		}
		textBox.enabled = active;
	}
}

public enum Speed
{
	Slow,
	Medium,
	Fast
}