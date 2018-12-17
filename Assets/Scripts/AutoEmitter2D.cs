using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines a delegate type for functions that require no parameters to generate an x-y coordinate
public delegate Vector2 Director2D();

/*
 * CLASS AutoEmitter2D : Emitter2D
 * -------------------------------
 * A type of emitter that allows a calling method to specify a rule by which
 * the emitter continually shoots automatically
 * -------------------------------
 */ 

public class AutoEmitter2D : Emitter2D
{
    [SerializeField]
    private float fireRateDifference;
    private Director2D aimGenerator;    // Function used to get the aim vector of the auto-emitter

    public void StartAutoEmitting (Director2D coordinates)
    {
        aimGenerator = coordinates;
        StopAllCoroutines();
        StartCoroutine("AutoEmitIterator", coordinates);
    }
    public void ResumeAutoEmitting ()
    {
        if(aimGenerator != null)
        {
            StopAllCoroutines();
            StartCoroutine("AutoEmitIterator", aimGenerator);
        }
        else
        {
            Debug.LogError("Cannot resume auto emitter on " + gameObject.name + ": have you called StartAutoEmitting yet?");
        }
    }
    public void StopAutoEmitting ()
    {
        StopAllCoroutines();
    }

    IEnumerator AutoEmitIterator (Director2D coordinates)
    {
        float delay;    // Delay between each emission

        // Store min-max times for how long it takes the emitter to emit
        float minTime = emitted.duration - fireRateDifference;
        float maxTime = emitted.duration + fireRateDifference;

        while (true)
        {
            delay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(delay);
            ForceEmit(coordinates());
        }
    }
}
