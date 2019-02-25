﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS Emitter2D : MonoBehaviour
 * -------------------------------
 * Emitters launch a group of moveable objects along the given aim vector
 * The origins and directions of each of the objects in the shot are
 * specified in a local list of origin-direction pairs. This makes the
 * emitter highly customizable and capable of producing complex emission
 * patterns, such as bullet spreads and parallel bullet shots
 * -------------------------------
 */ 

public class Emitter2D : MonoBehaviour
{
    [SerializeField]
    private GameObject emittedObject;
    [SerializeField]
    private ObjectPool<KinematicMover2D> pool;   // Emitter2D's object pool
    [SerializeField]
    private float _objectVelocity;   // Speed at which objects travel
    [SerializeField]
    private float emitRate; // Rate at which the emitter is allowed to emit 
    private State _emitted; // State returns true if the emitter emitted within #duration seconds of the current moment
    [SerializeField]
    private List<Anchor> objectAnchors; // Used to determine the local origin the objects start at and the direction they are fired off in relative to the emitter's aim 
    public event UnityAction<Vector2> onEmittedEvent;    // Event called whenever the the emitter emits

    public State emitted { get { return _emitted; } }

    protected virtual void Start()
    {
        _emitted = State.Construct(emitRate, gameObject);
        pool = new ObjectPool<KinematicMover2D>(emittedObject, gameObject.name + "'s Pool");
    }

    // Emit the objects using the local information
    // Aim vector is used such that objects going straight to the right go along the aim vector
    // Emits only if there has not been a recent emission
    public void Emit (Vector2 aimVector)
    {
        if(!_emitted)
        {
            ForceEmit(aimVector);
        }
    }

    // Force the emitter to emit, whether or not it emitted recently
    public void ForceEmit (Vector2 aimVector)
    {
        Vector2 rotatedOrigin;  // Origin of the current bullet, rotated by the aim vector
        Vector2 rotatedDirection;   // Direction of the current bullet, rotated by the aim vector
        float tiltAngle;    // Angle of the aim vector from the right

        tiltAngle = Vector2.SignedAngle(Vector2.right, aimVector);

        foreach (Anchor anchor in objectAnchors)
        {
            rotatedOrigin = anchor.origin.RotatedVector(tiltAngle);
            rotatedDirection = anchor.direction.RotatedVector(tiltAngle);
            pool.getOneQuick.Launch(rotatedOrigin + (Vector2)transform.position, rotatedDirection, _objectVelocity);
        }

        _emitted.Activate();

        // Call emitted event
        if(onEmittedEvent != null)
        {
            onEmittedEvent(aimVector);
        }
    }
}
