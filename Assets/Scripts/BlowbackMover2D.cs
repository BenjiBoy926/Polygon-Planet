using UnityEngine;
using System.Collections;

/*
 * CLASS BlowbackMover2D
 * ---------------------
 * A type of mover that can have its movement locked
 * by being blown back in a certain direction
 * ---------------------
 */ 

public class BlowbackMover2D : Mover2D, ISingleStateObject
{
    const float blowBackTime = 0.2f;    // Time for which blowback movers are blown back
    private State blownBack = new State(blowBackTime);    // State is true if the mover was recently blownback

    public State primaryState { get { return blownBack; } }

    // Move in the direction specified with the speed specified, and set the blownback state
    public void Blowback(Vector2 direction, float strength)
    {
        // Disable the blownback state and move according to the blowback method
        // This allows blowbacks to be chained one after the other
        blownBack.Deactivate();
        MoveTowards(direction, strength);

        // Activate the blowback state and schedule the object to stop moving when finished
        blownBack.Activate(blowBackTime);
        Invoke("StopBlowback", blowBackTime);
    }

    // Stop being blown back, and stop movement
    private void StopBlowback()
    {
        blownBack.Deactivate();
        Stop();
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
}
