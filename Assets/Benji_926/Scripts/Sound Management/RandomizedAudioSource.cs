using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS PlayRandomEffect
 * ----------------------
 * Much like an audio source, this script allows a set of effects
 * to be played at random either on a custom audio source or 
 * on an audio source on the sound player
 * ----------------------
 */ 

public class RandomizedAudioSource : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> clips;  // List of clips that are randomly played
    [SerializeField]
    private bool playOnAwake;   // If true, plays a random effect when enabled
    [SerializeField]
    private AudioSource source; // Source local to this object. If null, plays on a sound player local source

    private void OnEnable()
    {
        if(playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        // If we have an audio source, play on that
        if(source != null)
        {
            SoundPlayer.PlayRandomAudioClip(clips, source);
        }
        // Otherwise, play on one on the sound player
        else
        {
            SoundPlayer.instance.PlayRandomEffect(clips);
        }
    }
}
