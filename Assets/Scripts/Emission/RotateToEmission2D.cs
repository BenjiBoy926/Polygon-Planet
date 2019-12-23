using UnityEngine;

/*
 * CLASS RotateToEmission
 * ----------------------
 * Simple class causes the object attached to rotate to face
 * the direction the object is emitted
 * ----------------------
 */

public class RotateToEmission2D : MonoBehaviour
{
    [SerializeField]
    private Emitter2D emitter;
    [SerializeField]
    private Vector2 objectForward;
    [SerializeField]
    [Tooltip("The order in which the rotation occurs in the list of emission events")]
    private int order;

    private void Start()
    {
        emitter.emissionEvent.AddListener(LookTowardsEmissionListener, order);
    }

    private void LookTowardsEmission(Vector2 direction)
    {
        transform.LookInDirection2D(direction, objectForward);
    }
    private void LookTowardsEmissionListener(object directionObject)
    {
        LookTowardsEmission((Vector2)directionObject);
    }
}
