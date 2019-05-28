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

public class Emitter2D : MonoBehaviour, IEmitter
{
    [SerializeField]
    private GameObject emittedObject;
    [SerializeField]
    private ObjectPool<Rigidbody2D> pool;   // Emitter2D's object pool
    [SerializeField]
    private float _objectVelocity;   // Speed at which objects travel
    [SerializeField]
    private List<Anchor> objectAnchors; // Used to determine the local origin the objects start at and the direction they are fired off in relative to the emitter's aim 
    public event UnityAction<Vector2> emittedEvent;    // Event called whenever the the emitter emits

    protected virtual void Start()
    {
        pool = new ObjectPool<Rigidbody2D>(emittedObject, gameObject.name + "'s Pool");
    }

    // Emit the objects using the local information
    // Aim vector is used such that objects going straight to the right go along the aim vector
    // Emits only if there has not been a recent emission
    public virtual void Emit (Vector2 aimVector)
    {
        Vector2 localOrigin;  // Origin of the current bullet, rotated by the aim vector
        Vector2 force;   // Direction of the current bullet, rotated by the aim vector
        float tiltAngle;    // Angle of the aim vector from the right

        tiltAngle = Vector2.SignedAngle(Vector2.right, aimVector);

        // Rotate all origins and directions in the anchors by the tilt angle,
        // and add an impulse force to an object in the pool using rotated vectors
        foreach (Anchor anchor in objectAnchors)
        {
            localOrigin = anchor.origin.RotatedVector(tiltAngle);
            force = anchor.direction.RotatedVector(tiltAngle).ScaledVector(_objectVelocity);
            LaunchBody(pool.getOneQuick, localOrigin, force);
        }

        // Call emitted event
        if (emittedEvent != null)
        {
            emittedEvent(aimVector);
        }
    }

    // Simple helper moves the body to the position relative to this object 
    // and sets it off with an initial velocity
    private void LaunchBody(Rigidbody2D body, Vector2 localOrigin, Vector2 force)
    {
        body.gameObject.SetActive(true);
        body.transform.position = transform.position + (Vector3)localOrigin;
        body.velocity = force;
    }
}
