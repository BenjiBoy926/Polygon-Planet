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

public class AutoEmitter2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Script used to emit the objects")]
    private Emitter2D emitter;
    [SerializeField]
    [Tooltip("Min-max time between each emission")]
    private FloatConstraint emissionDelays;
    private Func<Vector2> aimGenerator;    // Function used to get the aim vector of the auto-emitter

    public void StartAutoEmitting (Func<Vector2> coordinates)
    {
        aimGenerator = coordinates;
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
        while (true)
        {
            delay = UnityEngine.Random.Range(emissionDelays.min, emissionDelays.max);
            yield return new WaitForSeconds(delay);
            emitter.Emit(aimGenerator());
        }
    }
}
