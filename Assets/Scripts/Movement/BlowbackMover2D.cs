using UnityEngine;
using System.Collections;

/*
 * CLASS BlowbackMover2D
 * ---------------------
 * A type of mover that can have its movement locked
 * by being blown back in a certain direction
 * ---------------------
 */ 

public class BlowbackMover2D : KinematicMover2D
{
    const float DEFAULT_BLOWBACK_TIME = 0.2f;    // Time for which all blowback movers are blown back
    private State _blownBack = new State(DEFAULT_BLOWBACK_TIME);    // State is true if the mover was recently blownback
    private Vector2 fromPointToHere = new Vector2();    // Points from the point of blowback to the position of this mover

    public State blownBack { get { return _blownBack; } }

    // Move in the direction, speed, and for the time specifed
    // Additional functionality locks movement of the mover until blowback is finished
    public void Blowback(Vector2 direction, float speed, float time = DEFAULT_BLOWBACK_TIME)
    {
        // Disable the blownback state and move according to the move towards method
        Stop();
        MoveTowards(direction, speed);

        // Activate the blowback state and schedule the object to stop moving when finished
        _blownBack.Activate(time);
        CancelInvoke();
        Invoke("Stop", time);
    }

    // Blowback methods blowback the mover from the specified point in world space, rather than with the specific direction
    public void BlowbackFromPoint(Vector2 point, float speed, float time = DEFAULT_BLOWBACK_TIME)
    {
        fromPointToHere = (Vector2)transform.position - point;
        Blowback(fromPointToHere, speed, time);
    }

    // Stop being blown back, and stop movement
    public override void Stop()
    {
        CancelInvoke();
        _blownBack.Deactivate();
        base.Stop();
    }

    // Overrides of the base class methods prevent client code from moving this object if it is currently being blown back
    public override void MoveTowards(Vector2 direction, float speed)
    {
        if(!_blownBack)
        {
            base.MoveTowards(direction, speed);
        }
    }
    public override void MoveToPoint(Vector2 point, float time)
    {
        if(!_blownBack)
        {
            base.MoveToPoint(point, time);
        }
    }
    public override void Launch(Vector2 origin, Vector2 direction, float speed)
    {
        if(!_blownBack)
        {
            base.Launch(origin, direction, speed);
        }
    }

    // "Force" methods allow client code the option to force the object to move regardless of whether or not it is being blown back right now
    public void ForceMoveTowards(Vector2 direction, float speed)
    {
        _blownBack.Deactivate();
        base.MoveTowards(direction, speed);
    }
    public void ForceMoveToPoint(Vector2 point, float time)
    {
        _blownBack.Deactivate();
        base.MoveToPoint(point, time);
    }
    public void ForceMoveFromPoint(Vector2 origin, Vector2 direction, float speed)
    {
        _blownBack.Deactivate();
        base.Launch(origin, direction, speed);
    }
}
