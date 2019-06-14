using UnityEngine;

/*
 * CLASS StateConstraint
 * ---------------------
 * Appliy a constraint to a constrainable object
 * based on a given state object
 * ---------------------
 */ 
public class StateConstraint : ConstraintApplier
{
    [SerializeField]
    [Tooltip("The state script used to constraint the constrainable object")]
    private State state;
    [SerializeField]
    [Tooltip("If true, return the opposite of the current state")]
    private bool inverted;

    protected override bool Constraint()
    {
        if (inverted)
        {
            return !state;
        }
        else
        {
            return state;
        }
    }
}
