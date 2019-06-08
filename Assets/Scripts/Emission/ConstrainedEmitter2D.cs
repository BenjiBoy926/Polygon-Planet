using UnityEngine;
using System;
using System.Collections.Generic;

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

public class ConstrainedEmitter2D : Emitter2D
{
    private List<Func<bool>> funcs = new List<Func<bool>>();

    // Emission is ready if all functions return true
    public bool emissionReady
    {
        get
        {
            bool ready = true;
            foreach(Func<bool> f in funcs)
            {
                ready &= f();
            }
            return ready;
        }
    }

    /*
     * PUBLIC INTERFACE
     */ 

    public void AddConstraint(Func<bool> constraint)
    {
        funcs.Add(constraint);
    }
        
    // Override prevents emissions based on the constraint
    public override void Emit(Vector2 aimVector)
    {
        if(emissionReady)
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
