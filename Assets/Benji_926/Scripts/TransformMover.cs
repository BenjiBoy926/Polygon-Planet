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
    private Transform trans;
    private Vector3 step;   // Vector that translates the transform each frame
    private int totalSteps; // Total number of steps the transform will take to get to its destination
    private int currentStep;    // Current step of that the transform is taking towards the destination
    private Space spaceOfMove;  // Space that the point the mover moves towards is interpreted in
    private bool isMoving;  // True if the mover is still moving to a point

    private void Update()
    {
        if(isMoving)
        {
            // Translate the transform out by one step
            trans.Translate(step, spaceOfMove);

            // Update current step. Keep moving if there are more steps to take
            ++currentStep;
            isMoving = currentStep <= totalSteps;
        }
    }

    // Prepare the mover to move slightly each frame in the Update method
    public void MoveToPoint2D(Vector2 point, float time, Space space = Space.Self)
    {
        Vector2 toTarget;   // Vector with its tip at the target and its tail at this object's position
        float stepMagnitude;    // The magnitude of one step is the magnitude of the whole path divided by the number of frames it'll take to get there

        // Calculate to target with local or global transform position
        if (space == Space.Self)
        {
            toTarget = point - (Vector2)trans.localPosition;
        }
        else
        {
            toTarget = point - (Vector2)trans.position;
        }

        // Calculate local variables and get the step vector that moves the transform
        totalSteps = Mathf.RoundToInt(time / Time.deltaTime);
        totalSteps = Mathf.Clamp(totalSteps, 1, 1000);
        stepMagnitude = toTarget.magnitude / totalSteps;
        step = toTarget.ScaledVector(stepMagnitude);

        isMoving = true;
        spaceOfMove = space;
        currentStep = 1;
    }

    // Stop any current motion
    public void Stop()
    {
        isMoving = false;
    }
}
