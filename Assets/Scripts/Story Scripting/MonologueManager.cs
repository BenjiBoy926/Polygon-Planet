using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Triggers the specified monologue at the start of the scene,
// and triggers an event when it finishes
public class MonologueManager : MonoBehaviour 
{
	[SerializeField]
	private Monologue monologue;	// The monologue to be trigggered at the start of the scene
	[SerializeField]
	private float waitTime;	// Time after starting to wait before starting the monologue
	[SerializeField]
	private string sceneName;	// Name of the scene to load if this is the player's first time viewing the lore
	[SerializeField]
	private int loreIndex;	// Represents the piece of lore being communicated in the monologue

	void Start ()
	{
		StartCoroutine (WaitInvokeMonologue ());
		monologue.SubscribeOnFinished (FinishMonologueAndAction);
	}

	// Start monologue after waiting for wait time seconds
	IEnumerator WaitInvokeMonologue ()
	{
		yield return new WaitForSeconds (waitTime);
		monologue.StartMonologue ();
	}

	// Load the scene specified and complete the specified action
	public void FinishMonologueAndAction ()
	{
        // If the lore has already been viewed before,
        // go back to the lore select scene
        if (DataManager.Data.LoreViewed[loreIndex - 1])
        {
            SceneManager.LoadScene("LoreSelect");
        }
        // Otherwise, load the special scene specified
        else
        {
            SceneManager.LoadScene(sceneName);
        }
		DataManager.MarkLoreAsViewed (loreIndex);
	}
}
