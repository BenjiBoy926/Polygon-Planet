using UnityEngine;
using System;

[Serializable]
public class BinaryNumericConstraint
{
    [SerializeField]
    [Tooltip("First number to compare with the given number")]
    private UnaryNumericConstraint _primaryConstraint;
    public UnaryNumericConstraint primaryConstraint { get { return _primaryConstraint; } }
    [SerializeField]
    [Tooltip("Second number to compare with the given number")]
    private UnaryNumericConstraint _secondaryConstraint;
    public UnaryNumericConstraint secondaryConstraint { get { return _secondaryConstraint; } }
    [SerializeField]
    [Tooltip("Method of combining the two unary operations")]
    private BinaryBooleanOperator operation;

    public bool Test(int number)
    {
        return Test(number, _primaryConstraint, _secondaryConstraint, operation);
    }

    public static bool Test(int number, UnaryNumericConstraint firstConstraint, UnaryNumericConstraint secondConstraint,
        BinaryBooleanOperator operation)
    {
        switch (operation)
        {
            case BinaryBooleanOperator.And:
                return firstConstraint.Test(number) && secondConstraint.Test(number);
            case BinaryBooleanOperator.Or:
                return firstConstraint.Test(number) || secondConstraint.Test(number);
            default:
                return false;
        }
    }
}
