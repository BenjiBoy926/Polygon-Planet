using UnityEngine;
using System.Collections.Generic;

// A component for a game object.  All of its methods delegate to the 
// central sound player that derives from the MonoSingleton class
public class SoundPlayerComponent : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Music played on the sound player " +
        "if the PlayMusic function is called through this script")]
    private List<AudioClip> music;

    public void PlayMusic(string theme)
    {
        SoundPlayer.instance.PlayMusic(theme, music);
    }
}
