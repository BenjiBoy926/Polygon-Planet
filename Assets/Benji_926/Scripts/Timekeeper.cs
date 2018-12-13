using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS Timekeeper
 * ----------------
 * Keeps track of time.  In this project, it pauses and resumes the game
 * ----------------
 */ 
public class Timekeeper : MonoBehaviour
{
	private static bool paused = false;	// True if the game is currently paused
	public static bool Paused { get { return paused; } }

	// Pause the game by setting the timescale to zero,
	// or unpause by setting it back to normal
	public static void PauseGame (bool isPausing)
	{
		// Set timescale to zero or one, depending on whether we're pausing
		if (isPausing) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}

		// Set local variable
		paused = isPausing;
	}
}
