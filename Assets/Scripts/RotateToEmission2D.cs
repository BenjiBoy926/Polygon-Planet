using UnityEngine;
using System.Collections;

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

    private void Start()
    {
        emitter.AddEmittedEvent(LookTowardsEmission);
    }

    private void LookTowardsEmission(Vector2 direction)
    {
        transform.LookInDirection2D(direction, objectForward);
    }
}
