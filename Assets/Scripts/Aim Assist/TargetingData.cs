using UnityEngine;
using UnityEditor;
using System;

/*
 * CLASS TargetingData
 * -------------------
 * Aim assisted bullets store an object of this class for each target in range
 * -------------------
 */ 

public class TargetingData : IComparable<TargetingData>
{
    private Transform _target;   // Transform position the object is aiming for
    private float _trajectoryAngle;  // Angle between the trajectory of the fired object and the target
    private Vector2 toTarget;   // Vector with its tail at the object and its tip at the target

    public Transform target { get { return _target; } }
    public float trajectoryAngle { get { return _trajectoryAngle; } }

    public TargetingData (Transform t)
    {
        _target = t;
    }

    // Calculate the angle between the trajectory of the object and the vector pointing to the target
    public void CalculateTrajectoryAngle(Rigidbody2D assistedObject)
    {
        // Calculate vector to target and angle between it and the trajectory
        toTarget = _target.position - assistedObject.transform.position;
        _trajectoryAngle = Vector2.SignedAngle(assistedObject.velocity, toTarget);
    }

    // Nudge the aim-asssisted object towards the target with the given strength
    public void NudgeToTarget(Rigidbody2D assistedObject, float strength)
    {
        assistedObject.velocity = assistedObject.velocity.RotatedVector(_trajectoryAngle * strength);
    }

    // Allows aim assist data to be sorted from least absolute to most absolute trajectory angle
    public int CompareTo (TargetingData other)
    {
        if(other == this)
        {
            return 0;
        }
        return (int)(Mathf.Abs(_trajectoryAngle) - Mathf.Abs(other._trajectoryAngle));
    }
}