using UnityEngine;

public class SupplySweepingVectorAroundTargetVector2D : SupplySweepingVector2D
{
    [SerializeField]
    [Tooltip("Reference to the script that gets the vector that points to the target")]
    private VectorToTarget2D vector;
    [SerializeField]
    [Tooltip("If true, the target vector automatically updates " +
        "each time a vector is requested. If false, the target " +
        "is not updated automatically and client code needs to call " +
        "UpdateTargetVector() whenever they want the target vector updated")]
    private bool updateTargetVectorAutomatically;

    // Current vector that points to the target
    private Vector2 currentToTargetVector;

    public void UpdateTargetVector()
    {
        currentToTargetVector = vector;
    }

    protected override Vector2 BaseVector()
    {
        if (updateTargetVectorAutomatically)
        {
            UpdateTargetVector();
        }
        return currentToTargetVector;
    }
}
