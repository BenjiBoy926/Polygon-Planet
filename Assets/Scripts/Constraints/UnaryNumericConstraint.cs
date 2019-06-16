using UnityEngine;
using System;

/*
 *  CLASS UnaryNumericConstraint
 *  ----------------------------
 *  Define rules for comparing a given number with another number
 *  ----------------------------
 */
 
public class UnaryNumericConstraint : MonoBehaviour, INumericConstraint
{
    [SerializeField]
    [Tooltip("The number to test agains the given number with the given comparison")]
    private int operand;
    [SerializeField]
    [Tooltip("The type of comparison to use against the given number")]
    private UnaryNumericComparison comparison;

    public UnaryNumericConstraint(int oper, UnaryNumericComparison comp)
    {
        operand = oper;
        comparison = comp;
    }

    public bool Test(int otherNum)
    {
        return Test(operand, otherNum, comparison);
    }

    public static bool Test(int firstNum, int secondNum, UnaryNumericComparison comparison)
    {
        switch (comparison)
        {
            case UnaryNumericComparison.GreaterThan:
                return secondNum > firstNum;
            case UnaryNumericComparison.GreaterThanOrEqualTo:
                return secondNum >= firstNum;
            case UnaryNumericComparison.EqualTo:
                return secondNum == firstNum;
            case UnaryNumericComparison.LessThanOrEqualTo:
                return secondNum <= firstNum;
            case UnaryNumericComparison.LessThan:
                return secondNum < firstNum;
            default:
                return false;
        }
    }
}
