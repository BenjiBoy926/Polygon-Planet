using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour 
{
	void Awake ()
	{
		if (!(DataManager.DataLoaded)) {
			DataManager.Load ();
		}
	}
}
