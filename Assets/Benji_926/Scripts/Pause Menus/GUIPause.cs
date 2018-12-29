using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS GUIPause : MonoBehaviour
 * ------------------------------
 * Describes an object that pauses the game and brings up
 * some GUI panel
 * ------------------------------
 */ 
public class GUIPause : MonoBehaviour 
{
	[SerializeField]
	private GameObject pausePanel;	// Panel brought up when GUI pause button is clicked
	[SerializeField]
	private AudioClip quickClip;	// Played when panel is activated/deactivated
	[SerializeField]
	private Canvas panelCanvas;	// Canvas the panel is a child of

	// Activate the panel, play sound effect, pause the game
	public void PanelAndPause (bool activate)
	{
		pausePanel.SetActive (activate);
		Timekeeper.instance.PauseGame (activate);
		SoundPlayer.Instance.PlaySoundEffect (quickClip);

		// If panel is activating, make the sorting order of the canvas arbitrarily large
		if (activate) {
			panelCanvas.sortingOrder = 10;
		}
		// Otherwise, make the sorting order zero
		else {
			panelCanvas.sortingOrder = 0;
		}
	}
}