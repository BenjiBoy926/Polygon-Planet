using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Describes any object with an animator that has a fade-in fade-out animation defined on it
public class FadingElement : MonoBehaviour 
{
	[SerializeField]
	private Animator anim;	// Animator on the fading element

	// Fade the object in, or fade it out
	public void Fade (bool active)
	{
		// Set animator trigger based on parameter
		if (active) {
			anim.SetTrigger ("Fade In");
		} else {
			anim.SetTrigger ("Fade Out");
		}
	}
}
