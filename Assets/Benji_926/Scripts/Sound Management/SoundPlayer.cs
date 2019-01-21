using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/*
 * CLASS SoundPlayer : MonoBehaviour
 * ---------------------------------
 * The central script from which all sound is played.
 * Designed to survive scene loading so that music plays
 * continuously through similarly-themed stages
 * ---------------------------------
 */ 

public class SoundPlayer : MonoSingleton<SoundPlayer> 
{
    // Constants determine the number of audio sources for each
    const int NUM_MUSIC_SOURCES = 2;
    const int NUM_SFX_SOURCES = 20;

    // Mixer groups that the sfx and music outputs to
    [SerializeField]
    private AudioMixerGroup musicMixer;
    [SerializeField]
    private AudioMixerGroup sfxMixer;

    private ObjectPool<AudioSource> musicSources;   // Pool of audio sources that play music
    private ObjectPool<AudioSource> sfxSources; // Pool of audio sources that play sound effects

	private MusicTheme _theme;	// Music theme currently being played by the sound player
	public MusicTheme theme { get { return _theme; } }

    [RuntimeInitializeOnLoadMethod]
	private static void InitializeSoundPlayer()
    {
        // Create the instance of the music player
        BaseCreateInstance("SoundPlayer");

        // Setup the two audio sources
        instance.musicSources = instance.SetupAudioPool(SoundType.Music);
        instance.sfxSources = instance.SetupAudioPool(SoundType.Effects);

        // Make sure audio source are equal at the start
        instance.musicSources.SetPoolActive(true);
        instance.sfxSources.SetPoolActive(true);
    }

	// Plays the given music clip of the specified theme
	public void PlayMusicOfTheme (MusicTheme newTheme, List<AudioClip> newMusic)
	{
        AudioSource currentMusicSource;  // Current music source having the clip set

		_theme = newTheme;

        // Loop through each music clip and get a music source to play it
		foreach(AudioClip clip in newMusic)
        {
            currentMusicSource = musicSources.getOneQuick;
            PlayAudioClip(clip, currentMusicSource);
        }

		// Log a warning if you specified too many new music audio clips
		if (newMusic.Count > musicSources.Count) {
			Debug.LogWarning ("You specified " + newMusic.Count + " music clips but the sound player can only play " + musicSources.Count);
		}
	}

	// Play the audio clip specified on an audio channel local to the sound player
	public void PlaySoundEffect (AudioClip effect)
	{
        AudioSource source = sfxSources.GetOne(AudioSourceIsNotPlaying);

        // If no source could be found that was not playing, log a warning
        if(source == null)
        {
            source = sfxSources.getOneQuick;
            Debug.LogWarning("The sound player ran out of sound effects sources to play effect " + effect.name);
        }

        PlayAudioClip(effect, source);
	}

    // Randomly select an audio clip to play from a list of clips
    public void PlayRandomEffect (List<AudioClip> clips)
	{
		int selection;	// Random selection from the list of clips
		selection = Random.Range (0, clips.Count);
		PlaySoundEffect (clips [selection]);
	}

    // Overlaod of PlaySoundEffect allows calling method to specify a unique audio source
    public static void PlayAudioClip(AudioClip effect, AudioSource source)
    {
        source.clip = effect;
        source.Play();
    }

    // Overload of PlayRandomEffect allows calling method to specify a unique audio source
    public static void PlayRandomAudioClip (List<AudioClip> clips, AudioSource source)
    {
        int selection;  // Random selection from the list of clips
        selection = Random.Range(0, clips.Count);
        PlayAudioClip(clips[selection], source);
    }

    // Specify a sound type and specify if it should be enabled or disabled
    public void ToggleSoundType (bool enabled, SoundType type)
	{
		ObjectPool<AudioSource> modPool;	// List of channels to be modified

		// Set list of channels to modify based on sound type specified
		if (type == SoundType.Music) {
			modPool = musicSources;
		} else {
			modPool = sfxSources;
		}

        // Mute or unmute each audio source in the pool to modify
		for(int index = 0; index < modPool.Count; ++index)
        {
            modPool[index].mute = !enabled;
        }
	}

	// Specify a sound type and specify a new volume for it
	public void AdjustVolumeOnSoundType (float newVolume, SoundType type)
	{
        ObjectPool<AudioSource> modPool;    // List of channels to be modified

        // Set list of channels to modify based on sound type specified
        if (type == SoundType.Music)
        {
            modPool = musicSources;
        }
        else
        {
            modPool = sfxSources;
        }

        // Set volumes on each of the audio sources in the pool to modify
        for(int index = 0; index < modPool.Count; ++index)
        {
            modPool[index].volume = newVolume;
        }
	}

    // Returns true if the given audio source is not currently playing
    private bool AudioSourceIsNotPlaying(AudioSource source)
    {
        return !source.isPlaying;
    }

    /*
     * PRIVATE HELPERS
     * ---------------
     * Private functions help the audio player set itself up
     * ---------------
     */ 

    // Return an object pool of audio sources for the set sound type
    private ObjectPool<AudioSource> SetupAudioPool(SoundType type)
    {
        PoolData data;  // Data used to setup the object pool

        if (type == SoundType.Effects)
        {
            data = SetupPoolData("SFX Channel", NUM_SFX_SOURCES, type);
        }
        else
        {
            data = SetupPoolData("Music Channel", NUM_MUSIC_SOURCES, type);
        }

        // Return a constructed object pool with the specified info
        return new ObjectPool<AudioSource>(data, transform);
    }

    // Setup pool data by constructing an object with the desired name
    // with the desired number of instances
    private PoolData SetupPoolData(string objectName, int numObjects, SoundType type)
    {
        // Empty object with an audio source that is instantiated by the object pool
        GameObject audioObject = new GameObject(objectName);
        audioObject.transform.parent = transform;
        SetupAudioSource(audioObject, type);

        // Return the pool data
        return new PoolData(audioObject, numObjects);
    }

    // Add an audio source to the game object specified
    // and apply the appropriate presets to it
    private void SetupAudioSource(GameObject audioObject, SoundType type)
    {
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Assign the correct mixer depending on the sound type
        if(type == SoundType.Effects)
        {
            audioSource.outputAudioMixerGroup = sfxMixer;
        }
        else
        {
            audioSource.outputAudioMixerGroup = musicMixer;
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