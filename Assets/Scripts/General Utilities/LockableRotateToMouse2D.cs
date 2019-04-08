using UnityEngine;
using System.Collections;

/*
 * CLASS LockableRotateToMouse2D
 * -----------------------------
 * A type of rotator that can have its rotation locked,
 * so that it will not rotate to the mouse unless unlocked
 * -----------------------------
 */ 

public class LockableRotateToMouse2D : RotateToMouse2D
{
    // True while the rotator is locked
    private State rotationLocked;

    protected override void Start()
    {
        base.Start();
        rotationLocked = State.Construct(theLabel: "RotationLock", obj: gameObject);
    }
    // Only update rotation if it isn't locked
    protected override void Update()
    {
        if(!rotationLocked)
        {
            base.Update();
        }
    }
    // Public events enable client code to lock the rotation
    public void LockRotation()
    {
        rotationLocked.Lock(true);
    }
    public void LockRotationForTime(float time)
    {
        rotationLocked.Unlock();
        rotationLocked.Activate(time);
    }
    public void UnlockRotation()
    {
        rotationLocked.Unlock();
    }
}
