using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS PlayerData
 * ----------------
 * Contains data that needs to be remembered between play sessions
 * Instances of this class are regularly stored on the machine via
 * the DataManager
 * ----------------
 */ 
[Serializable]
public class PlayerData
{
	[SerializeField]
	private List<bool> completedLevels;	// List of bools that reads true if the corresponding level has been completed
    [SerializeField]
    private List<bool> loreViewed;    // List of bools read true if the player has already seen that piece of the lore

	// Properties make variables read-only
	public List<bool> CompletedLevels { get { return completedLevels; } }
	public List<bool> LoreViewed { get { return loreViewed; } }

	// Constructor initializes data as if this was the player's first time playing
	public PlayerData (int totalLevels, int totalLores)
	{
		completedLevels = new List<bool> ();
        loreViewed = new List<bool>();

		for (int index = 0; index < totalLevels; index++)
        {
			completedLevels.Add (false);
		}

        for (int index = 0; index < totalLores; index++)
        {
            loreViewed.Add(false);
        }
	}

	// Complete the level specified
    // Does NOT use zero-indexing, expects levels 1 - #totalLevels
	public void CompleteLevel (int level)
	{
		completedLevels [level - 1] = true;
	}
    // Complete the lore specified
    // Does NOT use zero-indexing, expects lore 1 - #totalLores
	public void MarkLoreAsViewed (int lore)
    {
        loreViewed[lore - 1] = true;
    }
}