using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*
 * CLASS LevelLoader : MonoBehaviour
 * ---------------------------------
 * Describes a button that loads a scene when pressed.  Also displays
 * crucial information associated with the scene, like whether it is completed
 * ---------------------------------
 */ 

public class SceneLoader : MonoBehaviour
{
	private int lvl;	// Level this loader loads
    [SerializeField]
    private string scenePrefix; // String attached to the beginning of the scene name with the number added on
	[SerializeField]
	private Button loader;	// Button that loads the level when pressed
	[SerializeField]
	private Text levelDisplay;	// Displays the level this loader loads
	
	// Initilialize the level loader with the specified values
	public void Initialize (int level, bool playable, bool completed)
	{
		lvl = level;
		loader.interactable = playable;
		levelDisplay.text = level.ToString ();
	}

	// Load level associated with the script
	public void LoadLevel ()
	{
		SceneManager.LoadScene (scenePrefix + lvl);
	}
}
