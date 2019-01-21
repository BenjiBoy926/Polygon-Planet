using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS SoundSetup : MonoBehaviour
 * ---------------------------------
 * A script that loads an instance of the sound manager
 * if there is not already one in existence.  Also
 * sets the music to the theme specified for this scene
 * ---------------------------------
 */

/*
 * DEPRECIATED
 * -----------
 * The SoundPlayer inherits from the MonoSingleton class and automatically
 * instantiates an instance of itself when the game loads up
 * -----------
 */ 

public class SoundSetup : MonoBehaviour 
{
	[SerializeField]
	private GameObject soundPrefab;	// Prefab of the sound player
	[SerializeField]
	private List<AudioClip> musicClips;	// Clips for the music in this scene
	[SerializeField]
	private MusicTheme theme;	// Theme of the music in this scene
	//private SoundPlayer sound;	// Reference to sound player instantiated by this loader

	void Awake ()
	{
		// If SoundPlayer doesn't have a reference to itself yet,
		// we know we need to instantiate a copy of it 
		if (SoundPlayer.instance == null) {
			//sound = Instantiate (soundPrefab).GetComponent <SoundPlayer> ();
			//sound.Initialize ();
		}

        // If themes are different or not currently assigned, cause this theme to be played
        if ((SoundPlayer.instance.theme == MusicTheme.Unassigned || SoundPlayer.instance.theme != theme) && 
            musicClips.Count > 0 && theme != MusicTheme.Unassigned)
        {
            SoundPlayer.instance.PlayMusicOfTheme(theme, musicClips);
        }
	}
}
