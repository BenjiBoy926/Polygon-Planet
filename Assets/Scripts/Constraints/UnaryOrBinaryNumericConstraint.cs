using UnityEngine;
using System;

[Serializable]
public class UnaryOrBinaryNumericConstraint
{
    /*
     * PUBLIC TYPEDEFS 
     */

    public enum UnaryOrBinary { Unary, Binary };

    /*
     * PUBLIC DATA
     */ 
     
    [SerializeField]
    [Tooltip("Setup the rules for how the given number is tested")]
    private BinaryNumericConstraint constraint;
    [SerializeField]
    [Tooltip("Describe whether this is a unary or binary operation. " +
        "If unary, the secondary constraint on the BinaryNumericConstraint " +
        "object above is ignored")]
    private UnaryOrBinary type;
    
    public bool Test(int number)
    {
        switch (type)
        {
            case UnaryOrBinary.Unary:
                return constraint.primaryConstraint.Test(number);
            case UnaryOrBinary.Binary:
                return constraint.Test(number);
            default:
                return false;
        }
    }
}
