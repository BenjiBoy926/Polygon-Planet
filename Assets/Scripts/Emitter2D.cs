using System.Collections;
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
    private GameObject poolPrefab;  // Multiple instances of this object are instanitated in the object pool
    [SerializeField]
    private int poolSize;   // Size of the object pool for this emitter
    [SerializeField]
    private ObjectPool<Mover2D> pool;   // Emitter2D's object pool
    [SerializeField]
    private float objectVelocity;   // Speed at which objects travel
    [SerializeField]
    private State _emitted;    // State returns true if the emitter emitted within #duration seconds the current moment
    [SerializeField]
    private List<Anchor> objectAnchors; // Used to determine the local origin the objects start at and the direction they are fired off in relative to the emitter's aim 

    public State emitted { get { return _emitted; } }

    private void Start()
    {
        pool = new ObjectPool<Mover2D>(poolPrefab, poolSize, gameObject.name + "'s Pool");
        pool.SetPoolActive(false);
    }

    // Emit the objects in using the local information
    // Aim vector is used such that objects going straight to the right go along the aim vector
    public void Emit (Vector2 aimVector)
    {
        Vector2 rotatedOrigin;  // Origin of the current bullet, rotated by the aim vector
        Vector2 rotatedDirection;   // Direction of the current bullet, rotated by the aim vector
        float tiltAngle;    // Angle of the aim vector from the right

        tiltAngle = Vector2.SignedAngle(Vector2.right, aimVector);

        foreach (Anchor anchor in objectAnchors)
        {
            rotatedOrigin = anchor.origin.RotatedVector(tiltAngle);
            rotatedDirection = anchor.direction.RotatedVector(tiltAngle);
            pool.getReadyObject.MoveFromPoint(rotatedOrigin + (Vector2)transform.position, rotatedDirection, objectVelocity);
        }

        _emitted.Activate();
    }
}
