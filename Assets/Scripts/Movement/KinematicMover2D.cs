using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS KinematicMover2D
 * ----------------------
 * Enables an object with a 2D rigidbody to move within
 * the x-y plane by setting its velocity
 * ----------------------
 */

public class KinematicMover2D : MonoBehaviour
{
	[SerializeField]
    [Tooltip("Reference to the script that gets the game object " +
        "that the target rigidbody is attached to")]
	private GameObjectDelegate owningObject;

    private Rigidbody2D _rb2D;
    public Rigidbody2D rb2D
    {
        get
        {
            if(_rb2D == null)
            {
                _rb2D = owningObject.delegateObject.GetComponent<Rigidbody2D>();
            }
            return _rb2D;
        }
    }

    // Event called when the mover is stopped
    public event UnityAction stopEvent;

	// Set the velocity of the object in the direction specified with the speed specified
	public virtual void MoveTowards (Vector2 direction, float speed)
	{
		Vector2 velocity;
		velocity = direction.ScaledVector(speed);
		rb2D.velocity = velocity;
	}

    // Put the object at a position and set it off in a direction with a particular speed
    public virtual void Launch(Vector2 origin, Vector2 direction, float speed)
    {
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(origin.x, origin.y, transform.localPosition.z);
        MoveTowards(direction, speed);
    }

    // Uses local coroutine to move the object to the point specified,
    // set to arrive at the given time
    public virtual void MoveToPoint(Vector2 point, float time)
	{
		float speed;	// The speed the object needs to go to make it to the point in the time specified
		float dist;	// Distance between this object and its destination

		// Calculate the speed the object needs to go
		dist = (point - (Vector2)transform.localPosition).magnitude;
		speed = dist / time;

		// Use calculated speed in the coroutine
		StopCoroutine ("SmoothMove");
		StartCoroutine ("SmoothMove", new Waypoint(point, speed));
	}

	// Smoothly moves the object to the point, then stops
    // Please note that the algorithm breaks if something gets in the way of the object trying to move
	private IEnumerator SmoothMove(Waypoint point)
	{
		Func<bool> IsSufficientlyClose;	// Delegate returns true when the object is sufficiently close to the target
        WaitUntil waitUntil;    // Wait command used in the coroutine

		// Set velocity towards the direction
		MoveTowards(point.point - (Vector2)transform.localPosition, point.speed);

		// Set delegate to return true when the difference between
		// destination and current position is negligibly small
		IsSufficientlyClose = delegate {
			return (point.point - (Vector2)transform.localPosition).sqrMagnitude < 0.001f;
		};

        // Wait till object is close to the point, then stop moving
        waitUntil = new WaitUntil(IsSufficientlyClose);
		yield return waitUntil;
		Stop ();
	}

	// Stop coroutines and object motion
	public virtual void Stop ()
	{
		StopAllCoroutines ();
		rb2D.velocity = Vector2.zero;
        if(stopEvent != null)
        {
            stopEvent();
        }
	}
}

// CLASS Waypoint
// Pairs a two-point position with the speed at which the object will go to it
public struct Waypoint
{
	private Vector2 _point;
	private float _speed;

	public Vector2 point { get { return _point; } }
	public float speed { get { return _speed; } }

	// Basic constructor
	public Waypoint (Vector2 pnt, float spd)
	{
		_point = pnt;
		_speed = spd;
	}
}
