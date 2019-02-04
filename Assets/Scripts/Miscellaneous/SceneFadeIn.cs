using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Fades the scene in
public class SceneFadeIn : MonoBehaviour 
{
	[SerializeField]
	private FadingElement fader;

	void Start ()
	{
		fader.Fade (false);
	}
}
