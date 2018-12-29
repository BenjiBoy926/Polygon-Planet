using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private WaitUntil autoEmitWait;
    private Func<Vector2> aimGenerator;    // Function used to get the aim vector of the auto-emitter

    public void StartAutoEmitting (Func<Vector2> coordinates)
    {
        aimGenerator = coordinates;
        autoEmitWait = new WaitUntil(ReadyEmitNext);
        StopAllCoroutines();
        StartCoroutine("AutoEmitIterator");
    }
    public void ResumeAutoEmitting ()
    {
        if(aimGenerator != null)
        {
            StopAllCoroutines();
            StartCoroutine("AutoEmitIterator");
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

    IEnumerator AutoEmitIterator ()
    {
        float delay;    // Delay between each emission

        // Store min-max times for how long it takes the emitter to emit
        float minTime = emitted.duration - fireRateDifference;
        float maxTime = emitted.duration + fireRateDifference;

        while (true)
        {
            delay = UnityEngine.Random.Range(minTime, maxTime);
            emitted.Activate(delay);
            yield return autoEmitWait;
            ForceEmit(aimGenerator());
        }
    }

    private bool ReadyEmitNext()
    {
        return !emitted;
    }
}
