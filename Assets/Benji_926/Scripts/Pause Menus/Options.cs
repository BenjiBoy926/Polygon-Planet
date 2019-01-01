using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS Options : GUIPause
 * ------------------------
 * Allows various aspects of the SoundPlayer to be
 * muted and volume-tweaked using UI buttons and
 * sliders in the scene
 * ------------------------
 */ 

public class Options : GUIPause 
{
	[SerializeField]
	private AudioClip clickClip;	// Played when enabling object is clicked

	// Enable/disable music/sound effects by toggling the mute parameter
	public void EnableMusic (bool enabled)
	{
		SoundPlayer.instance.PlaySoundEffect (clickClip);
		SoundPlayer.instance.ToggleSoundType (enabled, SoundType.Music);
	}
	public void EnableEffects (bool enabled)
	{
		SoundPlayer.instance.PlaySoundEffect (clickClip);
		SoundPlayer.instance.ToggleSoundType (enabled, SoundType.Effects);
	}
	// Adjust volume for music/sound effects
	public void AdjustMusicVolume (float newVolume)
	{
		SoundPlayer.instance.AdjustVolumeOnSoundType (newVolume, SoundType.Music);
	}
	public void AdjustEffectsVolume (float newVolume)
	{
		SoundPlayer.instance.AdjustVolumeOnSoundType (newVolume, SoundType.Effects);
	}
}
