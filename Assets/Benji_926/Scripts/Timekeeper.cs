using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS Timekeeper
 * ----------------
 * Keeps track of time.  In this project, it pauses and resumes the game
 * ----------------
 */ 
public static class Timekeeper
{
	private static bool _paused = false;	// True if the game is currently paused
	public static bool paused { get { return _paused; } }

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
		_paused = isPausing;
	}
}
