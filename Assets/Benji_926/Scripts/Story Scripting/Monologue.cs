using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS Monologue : MonoBehaviour
 * -------------------------------
 * Manages a list of speech chunks by making each one display itself
 * gradually, one after the other
 * -------------------------------
 */
public class Monologue : MonoBehaviour 
{
	private int currentChunk = 0;	// Current speech chunk being displayed
	[SerializeField]
	private List<SpeechChunk> speechChunks;	// Chunks of speech used in the monologue
	[SerializeField]
	private float endSentencePause;	// Duration of an end-of-sentence pause
	[SerializeField]
	private float midSentencePause;	// Duration of a mid-sentence pause, like after a comma
	[SerializeField]
	private float chunkInterval;	// Time between the reveal of each chunk
	[SerializeField]
	private string skipButton;	// Name of the button that skips the speech chunk
	private bool skipReady = false;	// True if the current chunk is ready to be skipped so the next can begin
	private float skipReadyTimer = 0f;	// Used to see if we are ready to skip the current chunk
	private UnityAction onFinished;	// Event called when the monologue is finished
	[SerializeField]
	private List<AudioClip> voiceClips;	// A random clip is played from the list as the speech chunk is revealed
	[SerializeField]
	private GameObject skipPrompt;	// Prompts the user when the chunk is ready to be skipped

	// Returns a list of major punctuation marks
	public static List<char> punctuation {
		get {
			List<char> tempList = new List<char> ();	// Temporary list of characters to be returned
			tempList.Add ('.');
			tempList.Add ('!');
			tempList.Add ('?');
			tempList.Add (',');
			tempList.Add (';');
			tempList.Add (':');
			return tempList;
		} // END get
	} // END property

	private void Start ()
	{
		enabled = false;
		skipPrompt.SetActive (false);

		// Initialize chunks
		foreach (SpeechChunk chunk in speechChunks) {
			chunk.Initialize ();
		}
		SubscribeOnFinished (FinishMonologue);
	}

	private void Update ()
	{
		bool skipButtonPressed;	// True in the frame the skip button is pressed

		// Set skip ready
		skipReady = skipReadyTimer < Time.time && speechChunks [currentChunk].textFullyDisplayed;
		skipPrompt.SetActive (skipReady);

		// Set up input
		skipButtonPressed = Input.GetButtonDown (skipButton);

		if (skipButtonPressed) {
			// If the current speech chunk is skippable and isn't done displaying, display it
			if (speechChunks [currentChunk].Skippable && !(speechChunks [currentChunk].textFullyDisplayed)) {
				StopCoroutine ("GraduallyRevealChunk");
				ImmediatelyDisplayChunk (speechChunks [currentChunk]);
			}
			// Skip to the next chunk, if we're ready
			if (skipReady) {
				DisableDisplay ();
				currentChunk++;
				TryGradualReveal ();
			} // END if skip ready
		} // END if skip button pressed
	} // END method

	// Start the monologue
	public void StartMonologue ()
	{
		enabled = true;
		TryGradualReveal ();
	}

	// Try to gradually reveal the next speech chunk
	// If none are left, call the onFinished event
	private void TryGradualReveal ()
	{
		if (currentChunk < speechChunks.Count) {
			StopCoroutine ("GraduallyRevealChunk");
			StartCoroutine ("GraduallyRevealChunk", speechChunks [currentChunk]);
		} else {
			onFinished ();
		}
	}

	// Gradually reveal the speech chunk character by character
	private IEnumerator GraduallyRevealChunk (SpeechChunk chunk)
	{
		int currentChar = 0;	// Current character of the chunk being revealed
        WaitForSeconds revealWait = new WaitForSeconds(chunk.timeBetweenChars);
        WaitForSeconds endWait = new WaitForSeconds(endSentencePause);
        WaitForSeconds midWait = new WaitForSeconds(midSentencePause);

        // Activate display of the chunk
        chunk.DisplayActive (true);

		// Loop while current character is less than the characters in the text
		while (currentChar < chunk.theText.Length) {
			// Add a character to the display
			chunk.AddChar (currentChar);

			// If the character we added wasn't white space, play a sound effect
			if (chunk.theText [currentChar] != ' ') {
				SoundPlayer.Instance.PlayRandomEffect (voiceClips);
			}

			// Wait statements inside the selection
			// Don't bother waiting at all if the text is already full displayed
			if (!(chunk.textFullyDisplayed)) {
				// If the character added is a punctuation mark, wait for a little more time
				if (Monologue.punctuation.Contains (chunk.theText [currentChar])) {
					// Wait slightly longer if a sentence just ended 
					// as opposed to a mid-sentence pause with a comma or colon
					if (chunk.theText [currentChar] == '.' ||
					    chunk.theText [currentChar] == '!' ||
					    chunk.theText [currentChar] == '?') {
						yield return endWait;
					} else {
						yield return midWait;
					} // END if end-of-sentence punctuation
				} // END if punctuation

				yield return revealWait;
			} // END if fully displayed

			// Increment current character before moving on
			currentChar++;
		}

		// Start skip ready timer
		skipReady = false;
		skipReadyTimer = chunkInterval + Time.time;
	}

	// Immediately display the full text of the speech chunk specified
	private void ImmediatelyDisplayChunk (SpeechChunk chunk)
	{
		chunk.DisplayFullText ();
		skipReady = false;
		skipReadyTimer = chunkInterval + Time.time;
		SoundPlayer.Instance.PlayRandomEffect (voiceClips);
	}

	// Deactivate displays of all speech chunks
	private void DisableDisplay ()
	{
		foreach (SpeechChunk chunk in speechChunks) {
			chunk.DisplayActive (false);
		}
	}

	// Finish the monlogue by disabling the behaviour and display
	private void FinishMonologue ()
	{
		enabled = false;
		skipPrompt.SetActive (false);
		DisableDisplay ();
	}

	// Subscribe method specified to the on finished event
	public void SubscribeOnFinished (UnityAction method)
	{
		onFinished += method;
	}
}