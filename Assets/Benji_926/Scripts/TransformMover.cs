using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS TransformMover
 * --------------------
 * Analogous to the Kinematic mover, the Transform mover enables a game object
 * to be moved to points through directly manipulating the transform
 * --------------------
 */ 

public class TransformMover : MonoBehaviour
{
    [SerializeField]
    private new Transform transform;
    private Vector3 toTarget;   // Vector with its tip at the target and its tail at the starting position
    private Vector3 stepVector; // Vector that translates the transform each frame
    private bool isMoving;  // True if the transform is currently moving

    private float totalDistance;    // Total distance from current to target position
    private float distanceLeft; // Distance remaining for the transform to move
    private float inverseTime;  // INverse of the time it takes for the transform to move to its position

    private float currentStepDistance;  // Distance that the current step will take the transform component
    private Space currentMoveSpace; // Space that the current motion is being processed in

    private void Update()
    {
        if(isMoving)
        {
            // Calculate the current step distance, but make sure it does not exceed the distance left to travel
            currentStepDistance = totalDistance * Time.deltaTime * inverseTime;
            currentStepDistance = Mathf.Clamp(currentStepDistance, 0, distanceLeft);

            // Get the step vector and translate the transform with it
            stepVector = toTarget.ScaledVector(currentStepDistance);
            transform.Translate(stepVector, currentMoveSpace);

            // Decrease distance left by current step distance
            distanceLeft -= currentStepDistance;
            isMoving = distanceLeft > float.Epsilon;
        }
    }

    // Prepare the mover to move slightly each frame in the Update method
    public void MoveToPoint2D(Vector2 point, float time, Space space = Space.Self)
    {
        // Calculate to target with local or global transform position
        if (space == Space.Self)
        {
            toTarget = new Vector3(point.x, point.y, transform.localPosition.z) - transform.localPosition;
        }
        else
        {
            toTarget = new Vector3(point.x, point.y, transform.position.z) - transform.position;
        }

        // Set local variables to prepare the movement
        isMoving = true;

        totalDistance = toTarget.magnitude;
        distanceLeft = totalDistance;
        inverseTime = 1f / time;

        currentMoveSpace = space;        
    }

    // Stop any current motion
    public void Stop()
    {
        isMoving = false;
    }
}
