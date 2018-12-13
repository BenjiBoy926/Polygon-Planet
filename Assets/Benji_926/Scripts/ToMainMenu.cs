using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * CLASS ToMainMenu : GUIPause
 * ---------------------------
 * A type of gui pause object that directs the player to the
 * main menu when clicked 
 * ---------------------------
 */ 
public class ToMainMenu : GUIPause
{
	[SerializeField]
	private bool requireConfirmation;	// True if this button requires confirmation to go to the main menu
	[SerializeField]
	private string mainMenuName;	// Name of the main menu scene

	// If confirmation is required, activate the panel
	// otherwise, go ahead and load the main menu
	public void RequestConfirmation ()
	{
		if (requireConfirmation) {
			base.PanelAndPause (true);
		} else {
			LoadMainMenu ();
		}
	}

	// Loads the main menu by name
	public void LoadMainMenu ()
	{
		base.PanelAndPause (false);
		SceneManager.LoadScene (mainMenuName);
	}
}
