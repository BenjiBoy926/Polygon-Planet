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
    [SerializeField]
    [Tooltip("Total number of music sources on the sound player")]
    private int totalMusicSources = 2;
    [SerializeField]
    [Tooltip("Total number of sound effects sources on the sound player")]
    private int totalSFXSources = 20;

    [SerializeField]
    [Tooltip("Mixer that background music outputs to")]
    private AudioMixerGroup musicMixer;
    [SerializeField]
    [Tooltip("Mixer that sound effects output to")]
    private AudioMixerGroup sfxMixer;

    private ObjectPool<AudioSource> musicSources;   // Pool of audio sources that play music
    private ObjectPool<AudioSource> sfxSources; // Pool of audio sources that play sound effects

    // String descriptor of the current music theme being played
	public string currentMusicTheme { get; private set; }

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

    // Play the given music, but only if the given theme is unequal to the current theme
    public void PlayMusic(string theme, AudioClip music)
    {
        AudioClip[] clips = { music };
        PlayMusic(theme, new List<AudioClip>(clips));
    }
    public void PlayMusic (string theme, List<AudioClip> newMusic)
	{
        if(theme != currentMusicTheme)
        {
            ForcePlayMusic(theme, newMusic);
        }
	}

    // Play the given music, even if the current theme is the same as the incoming new theme
    public void ForcePlayMusic(string theme, List<AudioClip> newMusic)
    {
        AudioSource currentMusicSource;  // Current music source having the clip set

        StopMusic();
        currentMusicTheme = theme;
        int numSourcesToPlay = newMusic.Count < musicSources.Count ? newMusic.Count : musicSources.Count;

        // Loop through and play each music clip on each audio source
        for(int i = 0; i < numSourcesToPlay; i++)
        {
            currentMusicSource = musicSources[i];
            PlayAudioClip(newMusic[i], currentMusicSource);
        }

        // Log a warning if you specified too many new music audio clips
        if (newMusic.Count > musicSources.Count)
        {
            Debug.LogWarning("You specified " + newMusic.Count + " music clips but the sound player can only play " + musicSources.Count);
        }
    }

    public void StopMusic()
    {
        StopAudioPool(musicSources);
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

    public void StopSoundEffects()
    {
        StopAudioPool(sfxSources);
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
		if (type == SoundType.Music)
        {
			modPool = musicSources;
		}
        else
        {
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
            data = SetupPoolData("SFX Channel", totalSFXSources, type);
        }
        else
        {
            data = SetupPoolData("Music Channel", totalMusicSources, type);
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
            audioSource.loop = true;
        }
    }

    private void StopAudioPool(ObjectPool<AudioSource> pool)
    {
        for(int i = 0; i < pool.Count; i++)
        {
            pool[i].Stop();
        }
    }
}

public enum SoundType
{
	Music,
	Effects
}