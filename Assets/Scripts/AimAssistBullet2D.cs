using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS AimAssistBullet2D
 * -----------------------
 * A type of bullet that assists aiming by using an algorithm
 * to nudge its trajectory towards the location of any auto-aim
 * radii that it is inside of
 * -----------------------
 */ 

public class AimAssistBullet2D : Bullet2D
{
    private const float ASSIST_STRENGTH = 1 / 16f;  // A number between 0 and 1, where zero means no assistance and one means perfect aim
    private const float MAX_VIABLE_TARGET = 90f;    // If the trajectory angle is greater than or equal to this number, the bullet will not auto-assist towards it

    private Transform target;    // Current target that the bullet is trying to aim towards
    private Vector2 toTarget = new Vector2();   // Vector points from the bullet to the target
    [SerializeField]
    private Rigidbody2D rb2D;   // Rigidbody on this object
    [SerializeField]
    private string autoAimRadiusTag;    // Any object with a trigger that functions as an auto-aim radius is expected to be named with this tag

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        // If the target is unset, and this collider is an auto aim trigger...
        if(target == null && collision.CompareTag(autoAimRadiusTag))
        {
            //...set this collider to the target
            target = collision.transform;
        }
    }

    // Nudge the trajectory towards the target each frame 
    private void Update()
    {
        if(target != null)
        {
            NudgeToTarget();
        }
    }

    private void NudgeToTarget()
    {
        float trajectoryAngle;  // Angle between the toTarget vector and the bullet's current trajectory

        // Calculate vector to target and angle between it and the trajectory
        toTarget = target.position - transform.position;
        trajectoryAngle = Vector2.SignedAngle(rb2D.velocity, toTarget);

        // If the trajectory is too far off of the target, stop aiming for it
        if(Mathf.Abs(trajectoryAngle) >= MAX_VIABLE_TARGET)
        {
            target = null;
            return;
        }

        // Take a fraction of the trajectory angle and rotate the velocity by it
        trajectoryAngle *= ASSIST_STRENGTH;
        rb2D.velocity = rb2D.velocity.RotatedVector(trajectoryAngle);
    }
}
