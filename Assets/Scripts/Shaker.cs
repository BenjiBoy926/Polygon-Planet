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
    private const float DEFAULT_SHAKE_MAGNITUDE = 1f;
    private const float DEFAULT_SHAKE_TIME = 1f;
    private const float SHAKE_INTERVAL = 0.1f; // Seconds between each position change when the object is shaking

    private float shakeTime;    // Time for which the object will shake
    private float shakeMagnitude;   // Distance of the shaking of the object
    private Vector2 shakePos;   // Local position the object will be moved to when shaking
    private WaitForSeconds shakeWait = new WaitForSeconds(SHAKE_INTERVAL);   // Used to make the local coroutine wait the correct amount of time

    // Cause the object to shake around the local position with the desired strength for the desired time
    public void Shake(float magnitude = DEFAULT_SHAKE_MAGNITUDE, float time = DEFAULT_SHAKE_TIME)
    {
        shakeMagnitude = magnitude;
        shakeTime = time;

        // Start the coroutine
        StopAllCoroutines();
        StartCoroutine("ShakeIterator");
    }

    private IEnumerator ShakeIterator()
    {
        float activeTime = 0f;

        Random.InitState(0);

        while(activeTime < shakeTime)
        {
            shakePos = Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = new Vector3(shakePos.x, shakePos.y, transform.localPosition.z);
            yield return shakeWait;
            activeTime += SHAKE_INTERVAL;
        }
    }
}
