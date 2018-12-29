using UnityEngine;
using System.Collections;

/*
 * CLASS FixedAutoEmitter2D
 * ------------------------
 * A type of auto emitter that automatically emits based on a fixed aiming vector
 * ------------------------
 */ 

public class FixedAutoEmitter2D : AutoEmitter2D
{
    [SerializeField]
    private Vector2 aimVector;  // Fixed aiming vector of the auto emitter

    protected override void Start()
    {
        base.Start();
        StartAutoEmitting(AimVector);
    }

    private Vector2 AimVector()
    {
        return aimVector;
    }
}
