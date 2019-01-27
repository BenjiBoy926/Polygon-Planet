using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS FloatConstraint
 * ---------------------
 * Defines a min-max pair of floating point values
 * ---------------------
 */ 

[System.Serializable]
public struct FloatConstraint 
{
	public float min;
	public float max;

	public FloatConstraint (float minimum, float maximum)
	{
		min = minimum;
		max = maximum;
	}
}
