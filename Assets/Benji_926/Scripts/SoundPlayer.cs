using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS SoundPlayer : MonoBehaviour
 * ---------------------------------
 * The central script from which all sound is played.
 * Designed to survive scene loading so that music plays
 * continuously through similarly-themed stages
 * ---------------------------------
 */ 

public class SoundPlayer : MonoBehaviour 
{
	private static SoundPlayer instance;	// Reference to the single sound player active in every scene

	[SerializeField]
	private List<AudioSource> musicChannels;	// Audio sources that play continuous looping music
	[SerializeField]
	private List<AudioSource> sfxChannels;	// List of audio source channels that all play sound effects
	private MusicTheme theme;	// Music theme currently being played by the sound player
    private int internalIndex = 0;  // Index used to cycle through the SFX channels

	public static SoundPlayer Instance { get { return instance; } }
	public MusicTheme Theme { get { return theme; } }

	// Initialize the player by setting the singleton instance and
	// setting current music theme to unassigned
	public void Initialize ()
	{
		// If no sound object has yet been assigned, make this
		// the sound object and make sure it isn't destroyed on load
		if (instance == null) {
			DontDestroyOnLoad (gameObject);
			instance = this;
		}
		// Otherwise, if sound already exists but this isn't it, self-destruct
		else if (instance != this) {
			Destroy (gameObject);
		}

		// Initialize music theme to not yet assigned
		theme = MusicTheme.Unassigned;
	}

	// Plays the given music clip of the specified theme
	public void PlayMusicOfTheme (MusicTheme newTheme, List<AudioClip> newMusic)
	{
		theme = newTheme;

		// Go through the list of music channels and play the list of new music clips on each one
		for (int index = 0; index < musicChannels.Count; index++) {
			musicChannels [index].Stop ();

			if (index < newMusic.Count) {
				musicChannels [index].clip = newMusic [index];
				musicChannels [index].Play ();
			}
		}

		// Log a warning if you specified too many new music audio clips
		if (newMusic.Count > musicChannels.Count) {
			Debug.LogWarning ("You specified too many music clips for the sound player to play");
		}
	}

	// Play the audio clip specified on an audio channel local to the player
	public void PlaySoundEffect (AudioClip effect)
	{
        // Assign the current audio source channel into local var
        AudioSource source = sfxChannels[internalIndex];

        // If this source is already playing,
        // log a warning - we've run out of audio channels
        if (source.isPlaying)
        {
            Debug.LogWarning("You ran out of channels to play sound effects on");
        }

        // Play the clip specified on the next source
        source.clip = effect;
        source.Play();
        internalIndex++;

        // If the index is at or past total channels, reset it to zero
        if (internalIndex >= sfxChannels.Count)
        {
            internalIndex = 0;
        }
	}

    // Overlaod of PlaySoundEffect allows calling method to specify a unique audio source
    public static void PlaySoundEffect(AudioClip effect, AudioSource source)
    {
        source.clip = effect;
        source.Play();
    }

    // Randomly select an audio clip to play from a list of clips
    public void PlayRandomEffect (List<AudioClip> clips)
	{
		int selection;	// Random selection from the list of clips
		selection = Random.Range (0, clips.Count);
		PlaySoundEffect (clips [selection]);
	}

    // Overload of PlayRandomEffect allows calling method to specify a unique audio source
    public static void PlayRandomEffect (List<AudioClip> clips, AudioSource source)
    {
        int selection;  // Random selection from the list of clips
        selection = Random.Range(0, clips.Count);
        PlaySoundEffect(clips[selection], source);
    }

    // Specify a sound type and specify if it should be enabled or disabled
    public void ToggleSoundType (bool enabled, SoundType type)
	{
		List<AudioSource> modChannels;	// List of channels to be modified

		// Set list of channels to modify based on sound type specified
		if (type == SoundType.Music) {
			modChannels = musicChannels;
		} else {
			modChannels = sfxChannels;
		}

		// Mute/unmute each channel in the list of channels to modify
		foreach (AudioSource channel in modChannels) {
			channel.mute = !enabled;
		}
	}

	// Specify a sound type and specify a new volume for it
	public void AdjustVolumeOnSoundType (float newVolume, SoundType type)
	{
		List<AudioSource> modChannels;	// List of channels to be modified

		// Set list of channels to modify based on sound type specified
		if (type == SoundType.Music) {
			modChannels = musicChannels;
		} else {
			modChannels = sfxChannels;
		}

		// Mute/unmute each channel in the list of channels to modify
		foreach (AudioSource channel in modChannels) {
			channel.volume = newVolume;
		}
	}
}

public enum MusicTheme
{
	Unassigned,
	Level,
	Menu
}

public enum SoundType
{
	Music,
	Effects
}