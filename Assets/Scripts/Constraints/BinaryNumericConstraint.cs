using UnityEngine;
using System;

/*
 * CLASS BinaryNumericConstraint
 * -----------------------------
 * Defines a set of parameters to use to compare a given number
 * with two other numbers
 * -----------------------------
 */ 

public class BinaryNumericConstraint : MonoBehaviour, INumericConstraint
{
    [SerializeField]
    [Tooltip("First number to compare with the given number")]
    private int operand1;
    [SerializeField]
    [Tooltip("Second number to compare with the given number")]
    private int operand2;
    [SerializeField]
    [Tooltip("Method of comparing the given number with the other two operands")]
    private BinaryNumericComparison operation;

    public bool Test(int number)
    {
        return Test(number, operand1, operand2, operation);
    }

    public static bool Test(int number, int lowerNum, int higherNum,
        BinaryNumericComparison operation)
    {
        // Make sure the integer in higher num is actually the larger of the two
        if (lowerNum > higherNum)
        {
            int temp = lowerNum;
            lowerNum = higherNum;
            higherNum = temp;
        }

        // Return 
        switch (operation)
        {
            case BinaryNumericComparison.BetweenExclusive:
                return number > lowerNum && number < higherNum; 
            case BinaryNumericComparison.BetweenInclusive:
                return number >= lowerNum && number <= higherNum;
            case BinaryNumericComparison.OutsideExclusive:
                return number < lowerNum || number > higherNum;
            case BinaryNumericComparison.OutsideInclusive:
                return number <= lowerNum || number >= higherNum;
            default:
                return false;
        }
    }
}
