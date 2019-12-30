using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class ExtensionMethods 
{	
	// Returns a two-dimensional vector going in the same direction
	// as baseVector but with the specified magnitude
	public static Vector2 ScaledVector (this Vector2 baseVector, float magnitude)
	{
        // X/Y-component of vector to be returned
        float newX = 0;
        float newY = 0;
        float baseSqrMagnitude = baseVector.sqrMagnitude;

		// Check if base vector is negligibly small to prevent divide by zero error
		if (baseSqrMagnitude < -Mathf.Epsilon || baseSqrMagnitude > Mathf.Epsilon)
        {
			// Inverse of the magnitude; stored for efficiency
			float inverseBaseMagnitude = 1f / baseVector.magnitude;

			// Calculate the components to be returned using ratio of corresponding parts
			// (given vector and desired vector form similar triangles, remember?)
			newX = magnitude * baseVector.x * inverseBaseMagnitude;
			newY = magnitude * baseVector.y * inverseBaseMagnitude;
		} // END if

		return new Vector2(newX, newY);
	} // END method

    public static Vector3 ScaledVector (this Vector3 baseVector, float magnitude)
    {
        // Declare x/y/z of new vector and set them to zero
        float newX, newY, newZ;
        newX = newY = newZ = 0;
        float baseSqrMagnitude = baseVector.sqrMagnitude;

        // Check to make sure base vector is not too small - prevent divide by zero
        if (baseSqrMagnitude < -Mathf.Epsilon || baseSqrMagnitude > Mathf.Epsilon)
        {
            // Store invers of magnitude for efficiency
            float inverseBaseMagnitude = 1f / baseVector.magnitude;

            // Calculate components using ratio of corresponding parts
            newX = magnitude * baseVector.x * inverseBaseMagnitude;
            newY = magnitude * baseVector.y * inverseBaseMagnitude;
            newZ = magnitude * baseVector.z * inverseBaseMagnitude;
        }

        return new Vector3(newX, newY, newZ);
    }

    // Return a version of the vector passed in, rotated theta DEGREES counter-clockwise
    public static Vector2 RotatedVector (this Vector2 v, float theta)
    {
        float initAngle = Vector2.Angle(v, Vector2.right);  // V's initial angle from the x-axis
        float componentAngle;   // Angle used to calculate v's new x-y components
        float magnitude = v.magnitude;  // Magnitude of the vector, queued for efficiency

        // If v is in the 3rd or 4th quadrants,
        // we know the angle should be negative
        if (v.y < 0f)
        {
            initAngle *= -1f;
        }

        // Calculate and convert the angle used to find the new x-y components
        componentAngle = initAngle + theta;
        componentAngle *= Mathf.Deg2Rad;

        // Return the sign and cosine of the angle
        return new Vector2(magnitude * Mathf.Cos(componentAngle), magnitude * Mathf.Sin(componentAngle));
    }

	// Convert an array of a specified type to a list of the same type
	public static List<T> ToList<T> (this T[] array)
	{
		// Define a local list to return
		List<T> localList = new List<T> ();

		// Add each item in the array to the list
		foreach (T item in array) {
			localList.Add (item);
		} // END foreach

		return localList;
	} // END method

	public static void Reset (this Transform trans)
	{
		trans.position = Vector3.zero;
		trans.rotation = Quaternion.Euler (Vector3.zero);
		trans.localScale = Vector3.one;
	}

    // Given a direction and the forward-facing vector of the transform,
    // rotate the transform to look at the direction specified
    // Function assumes 2D playing field is in the x-y plane
    public static void LookInDirection2D(this Transform trans, Vector2 direction, Vector2 forward)
    {
        float angle = Vector2.SignedAngle(forward, direction);
        trans.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public static Vector2 ToTarget2D(this Transform me, Transform target)
    {
        return target.position - me.position;
    }
    public static Vector3 ToTarget(this Transform me, Transform target)
    {
        return target.position - me.position;
    }

    /*
     *  TRANSFORM 
     */ 

    public static void ForEachChild(this Transform ego, UnityAction<Transform> action)
    {
        Transform current;

        for(int i = 0; i < ego.childCount; i++)
        {
            current = ego.GetChild(i);

            // If the current child has children, run the Foreach on their children
            if (current.childCount > 0)
            {
                current.ForEachChild(action);    
            }

            // Run the action on this child
            action(current);
        }
    }

    public static Transform FindChildMatch(this Transform ego, System.Predicate<Transform> predicate)
    {
        Transform current;

        for(int i = 0; i < ego.childCount; i++)
        {
            current = ego.GetChild(i);

            // If current child matches the condition, return it
            if(predicate(current))
            {
                return current;
            }
            // If current child has children, find one that has a match
            else if(current.childCount > 0)
            {
                current = current.FindChildMatch(predicate);
                if (current != null)
                {
                    return current;
                }
            }
        }

        return null;
    }

    /*
     * STRING
     */

    // Find the first integer that occurs in the string, and return the integer
    public static int FirstInteger(this string str)
    {
        return int.Parse(str.FirstIntegerString());
    }

    public static string FirstIntegerString(this string str)
    {
        char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '+' };
        int firstIndex = str.IndexOfAny(numbers);

        if (firstIndex >= 0)
        {
            List<char> numberList = new List<char>(numbers);
            int lastIndex;

            numberList.RemoveRange(10, 2);

            // Loop until the end of the string,
            // or until a character is found that is not a number
            for (
                lastIndex = firstIndex;
                lastIndex < str.Length &&
                numberList.Find(x => x == str[lastIndex]) >= 0;
                lastIndex++) ;

            return str.Substring(firstIndex, lastIndex - firstIndex + 1);
        }
        else
        {
            return null;
        }
    }

    
}
