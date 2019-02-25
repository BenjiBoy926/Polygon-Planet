using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS AimAssistBullet2D
 * -----------------------
 * Automatically adjusts the trajectory of the rigidbody given 
 * to nudge a little bit closer to the optimal target in range
 * -----------------------
 */ 

public class AimAssist : MonoBehaviour
{
    private const float ASSIST_STRENGTH = 20f;  // A number between 0 and 1, where zero means no assistance and one means perfect aim
    private const float MAX_VIABLE_TARGET = 90f;    // If the trajectory angle is greater than or equal to this number, the bullet will not auto-assist towards it

    [SerializeField]
    private Rigidbody2D rb2D;   // Rigidbody on this object
    [SerializeField]
    private string autoAimRadiusTag;    // Any object with a trigger that functions as an auto-aim radius is expected to be named with this tag

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If this is an auto aim radius, aim assist towards it
        if(collision.CompareTag(autoAimRadiusTag))
        {
            NudgeToTarget(collision.transform);
        }
    }

    private void NudgeToTarget(Transform target)
    {
        Vector2 toTarget;   // Vector with the tip at the target and the tail at this object
        float trajectoryAngle;  // Angle between the trajectory of this object and the toTarget vector

        // Calculate vector to target and angle between it and the trajectory
        toTarget = target.position - transform.position;
        trajectoryAngle = Vector2.SignedAngle(rb2D.velocity, toTarget);

        // If the target is not too far off, nudge towards it
        if (trajectoryAngle <= MAX_VIABLE_TARGET)
        {
            float trajectoryNudge;  // Angle by which trajectory is nudged

            // Makes sure the assist strength matches the sign of the trajectory angle
            if (trajectoryAngle < 0f)
            {
                trajectoryNudge = MyMath.ClosestToZero(trajectoryAngle, -ASSIST_STRENGTH);
            }
            else
            {
                trajectoryNudge = MyMath.ClosestToZero(trajectoryAngle, ASSIST_STRENGTH);
            }

            // Rotate the velocity by the nudge
            rb2D.velocity = rb2D.velocity.RotatedVector(trajectoryNudge);
        }
    }
}
