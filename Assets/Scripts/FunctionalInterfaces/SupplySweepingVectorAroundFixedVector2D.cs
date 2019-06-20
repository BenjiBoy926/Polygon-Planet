using UnityEngine;

public class SupplySweepingVectorAroundFixedVector2D : SupplySweepingVector2D
{
    [SerializeField]
    [Tooltip("The fixed vector that the supplied vector sweeps over")]
    private Vector2 fixedVector;

    protected override Vector2 BaseVector()
    {
        return fixedVector;
    }
}
