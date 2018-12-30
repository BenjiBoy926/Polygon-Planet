using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct IntConstraint
{
    public int min;
    public int max;

    public IntConstraint(int minimum, int maximum)
    {
        min = minimum;
        max = maximum;
    }
}
