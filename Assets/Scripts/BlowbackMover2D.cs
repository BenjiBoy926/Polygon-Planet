using UnityEngine;
using System.Collections;

/*
 * CLASS BlowbackMover2D
 * ---------------------
 * A type of mover that can have its movement locked
 * by being blown back in a certain direction
 * ---------------------
 */ 

public class BlowbackMover2D : Mover2D
{
    const float blowBackTime = 0.2f;    // Time for which all blowback movers are blown back
    private State blownBack = new State(blowBackTime);    // State is true if the mover was recently blownback

    public State primaryState { get { return blownBack; } }

    // Blowback for the constant time locally specified
    public void Blowback(Vector2 direction, float speed)
    {
        Blowback(direction, speed, blowBackTime);
    }

    // Move in the direction, speed, and for the time specifed
    // Additional functionality locks movement of the mover until blowback is finished
    public void Blowback(Vector2 direction, float speed, float time)
    {
        // Disable the blownback state and move according to the move towards method
        Stop();
        MoveTowards(direction, speed);

        // Activate the blowback state and schedule the object to stop moving when finished
        blownBack.Activate(blowBackTime);
        Invoke("Stop", time);
    }

    // Stop being blown back, and stop movement
    public override void Stop()
    {
        CancelInvoke();
        blownBack.Deactivate();
        base.Stop();
    }

    // Overrides of the base class methods prevent client code from moving this object if it is currently being blown back
    public override void MoveTowards(Vector2 direction, float speed)
    {
        if(!blownBack)
        {
            base.MoveTowards(direction, speed);
        }
    }
    public override void MoveToPoint(Vector2 point, float time)
    {
        if(!blownBack)
        {
            base.MoveToPoint(point, time);
        }
    }
    public override void MoveFromPoint(Vector2 origin, Vector2 direction, float speed)
    {
        if(!blownBack)
        {
            base.MoveFromPoint(origin, direction, speed);
        }
    }

    // "Force" methods allow client code the option to force the object to move regardless of whether or not it is being blown back right now
    public void ForceMoveTowards(Vector2 direction, float speed)
    {
        blownBack.Deactivate();
        base.MoveTowards(direction, speed);
    }
    public void ForceMoveToPoint(Vector2 point, float time)
    {
        blownBack.Deactivate();
        base.MoveToPoint(point, time);
    }
    public void ForceMoveFromPoint(Vector2 origin, Vector2 direction, float speed)
    {
        blownBack.Deactivate();
        base.MoveFromPoint(origin, direction, speed);
    }
}
