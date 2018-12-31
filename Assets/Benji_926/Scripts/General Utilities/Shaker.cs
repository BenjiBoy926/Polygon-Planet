using UnityEngine;
using System.Collections;

/*
 * CLASS Shaker
 * ------------
 * Simple class attached to an object can make it shake around
 * some local position
 * ------------
 */ 

public class Shaker : MonoBehaviour
{
    [SerializeField]
    private TransformMover mover;   // Used to move the object that will shake around

    // Default parameters used if the calling method does not want to specify
    [SerializeField]
    private float defaultShakeTime;
    [SerializeField]
    private float defaultShakeMagnitude;
    [SerializeField]
    private float defaultShakeInterval;

    private float shakeTime;    // Time for which the object will shake
    private float shakeMagnitude;   // Distance of the shaking of the object
    private float shakeInterval;    // Interval between shakes - smaller intervals makes mor violent shakes

    private Vector2 shakePos;   // Local position the object will be moved to when shaking

    // Use default parameters
    public void Shake2D()
    {
        Shake2D(defaultShakeMagnitude, defaultShakeTime, defaultShakeInterval);
    }

    // Cause the object to shake around the local position with the desired strength for the desired time
    public void Shake2D(float magnitude, float time, float interval)
    {
        shakeMagnitude = magnitude;
        shakeTime = time;
        shakeInterval = interval;

        // Start the coroutine
        StopAllCoroutines();
        StartCoroutine("ShakeIterator");
    }

    private IEnumerator ShakeIterator()
    {
        WaitForSeconds shakeWait = new WaitForSeconds(shakeInterval);
        float activeTime = 0f;

        Random.InitState(0);

        while(activeTime < shakeTime)
        {
            // Get a random position and scale it up by the magnitude
            shakePos = Random.insideUnitCircle * shakeMagnitude;
            mover.MoveToPoint2D(shakePos, shakeTime);

            // Wait util this shake is finished
            yield return shakeWait;
            activeTime += shakeInterval;
        }

        // Reset the local position of the object after shaking it around
        mover.Stop();
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
    }
}
