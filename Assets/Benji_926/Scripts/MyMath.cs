using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS MyMath
 * ------------
 * Additional mathy methods that could be in Mathf
 * except that they're way too specific for that
 * ------------
 */ 

public static class MyMath
{
	// Finds the vertices of a regular polygon inscribed in a circle of given radius.  Optionally, the calling method can specify
	// the center of the circle and rotation of the polygon going counterclockwise from the right
	public static List<Vector2> VerticesOfInscribedNGon (int n, float radius, Vector2 center = default(Vector2), float phase = default(float), CircleDir dir = CircleDir.CounterClockwise)
	{
		/*
		 * BEGIN ERROR CHECKS
		 */ 
		// If n is less than or equal to 2, the results of this function
		// are not meaningful, so exit without calculation
		if (n <= 2) {
			Debug.LogWarning ("You cannot meaningfully inscribe a figure with 2 or less vertices inside of a circle");
			return default(List<Vector2>);
		}

		// Don't do anything if the radius is zero or less
		if (radius < Mathf.Epsilon) {
			Debug.LogWarning ("You cannot inscribe any figure inside of a circle with a radius that is zero or less");
			return default(List<Vector2>);
		}
		/*
		 * END ERROR CHECKS
		 */ 

		// Vertices of the n-gon translated by the center of the circle
		List<Vector2> globalVertices = new List<Vector2> ();
		Vector2 localVertex;	// A vertex of the n-gon relative to the center of the circle
		float angle;	// The value of theta + phase, where theta = (2 * pi * k) / n
		float dirConst;	// A constant set to 1f or -1f depending on whether the angle goes counter-clockwise or clockwise, respectively
		float twoPi = 2f * Mathf.PI;	// The value of 2 * pi, stored for convenience and efficiency
		float inverseN = 1f / (float)n;	// The inverse of n, stored for convenience and efficiency

		// Make directional constant 1 or -1 depending on whether we're
		// listing the vertices in the clockwise or counter-clockwise direction
		dirConst = (dir == CircleDir.Clockwise) ? -1f : 1f;

		for (int k = 0; k < n; k++) {
			// Current angle is the full circle in radians divided by n (or in this case multiplied by inverse of n)
			// multiplied by each number from 0 leading up to but not including n, plus the phase
			angle = ((twoPi * k * inverseN) + phase) * dirConst;
			// Use sines and cosines to get the vertices assuming a circle with the given radius at the origin
			localVertex = new Vector2 (radius * Mathf.Cos (angle), radius * Mathf.Sin (angle));
			// Add the local vertex to the center coordinate given
			globalVertices.Add (localVertex + center);
		} // END for

		return globalVertices;
	} // END method
}

public enum CircleDir
{
    Clockwise,
    CounterClockwise
}