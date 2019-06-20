using UnityEngine;

public class TransformLookInDirection2D : MonoBehaviour, IConsumer<Vector2>
{
    [SerializeField]
    [Tooltip("Transform component to rotate to the consumed Vector2")]
    private Transform trans;
    [SerializeField]
    [Tooltip("Defines the forward direction for the transform component")]
    private Vector2 forward;

    public void Consume(Vector2 direction)
    {
        trans.LookInDirection2D(direction, forward);
    }
}
