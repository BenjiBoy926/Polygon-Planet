using UnityEngine;
using System.Collections;

/*
 * CLASS EventEffector
 * -------------------
 * A simple "bridge class" deriving from the ParicleEffector
 * that takes the transform of an object (called the "event object")
 * and grabs the component of the desired type on that object
 * -------------------
 */ 

public class EventEffector<EventHandle> : ParticleEffector
{
    [SerializeField]
    protected Transform eventObject;    // Reference to the object where the effect activates
    protected EventHandle eventHandle;   // Reference to a script on the event object that calls an event that produces

    protected override void Start()
    {
        base.Start();
        eventHandle = eventObject.GetComponentInChildren<EventHandle>();
    }
}
