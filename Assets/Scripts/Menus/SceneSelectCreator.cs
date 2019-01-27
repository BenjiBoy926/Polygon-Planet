using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * CLASS SceneSelectManager : MonoBehaviour
 * ----------------------------------------
 * A script that instantiates multiple instances of scene loaders
 * ----------------------------------------
 */ 

public class SceneSelectCreator : MonoBehaviour 
{
	[SerializeField]
	private Transform selectorHolder;	// Transform that holds the stage loaders
	[SerializeField]
	private GameObject selectorPrefab;	// Prefab of the button used to load the scene specified
    [SerializeField]
    private bool oneAfterOn;    // True if the scene selectors are activated such that the one after the latest completed scene is available
                                // If false, only completed scenes are available

	// Instantiate buttons to navigate between stages. Requires a list of bools
    // representing which of the scenes have been completed
	public void InitializeSceneSelectors (List<bool> sceneCompleted)
	{
		SceneLoader loader;	// Script that describes a scene loading button
		bool playable;	// True if the current scene loader button is interactable

		// Loop through total levels and instantiate a button for each
		for (int level = 0; level < sceneCompleted.Count; level++) {
			loader = Instantiate (selectorPrefab, selectorHolder).GetComponent <SceneLoader> ();

			if (level > 0) {
                // The scene is loadable if it has been completed already,
                // or if the one before it was completed already and the one after is supposed to be active
				playable = sceneCompleted[level] || sceneCompleted [level - 1] && oneAfterOn;
			} else {
                // If this is the first scene, it's playable if the one after is suppsed to be active
				playable = sceneCompleted[level] || oneAfterOn;
			}

			// Initialize the loader instantiated
			loader.Initialize (level + 1, playable, sceneCompleted[level]);
		} // END for loop
	} //  END method
}
