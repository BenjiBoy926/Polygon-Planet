using UnityEngine;
using System;

/*
 * CLASS ConstrainedEmitter2D
 * --------------------------
 * A type of emitter with a function pointer that returns true
 * if the emitter can emit and false if it cannot. Also has
 * useful public methods for "and"-ing and "or"-ing other
 * functions with the constraint to create an emitter with
 * multiple constraints
 * --------------------------
 */ 

public class ConstrainedEmitter2D : Emitter2D, IConstrainable
{
    private BooleanFunctorList _constraints = new BooleanFunctorList();
    public BooleanFunctorList constraints { get { return _constraints; } }

    /*
     * PUBLIC INTERFACE
     */ 

    public void AddConstraint(Func<bool> constraint)
    {
        _constraints.Add(constraint);
    }
        
    // Override prevents emissions based on the constraint
    public override void Emit(Vector2 aimVector)
    {
        if(_constraints.result)
        {
            ForceEmit(aimVector);
        }
    }
    // Optionally force the emitter to emit, ignoring the constraint
    public void ForceEmit(Vector2 aimVector)
    {
        base.Emit(aimVector);
    }
}
