using UnityEngine;
using System;

public class SupplyVectorToTarget2D : MonoBehaviour, ISupplier<Vector2>
{
    [SerializeField]
    [Tooltip("Info needed to get the vector with its tail at an object and its tail at another object")]
    private VectorToTarget2D vector;

    public Vector2 Supply()
    {
        return vector;
    }
}
