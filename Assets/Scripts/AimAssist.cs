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
    private const float ASSIST_STRENGTH = 1 / 16f;  // A number between 0 and 1, where zero means no assistance and one means perfect aim
    private const float MAX_VIABLE_TARGET = 90f;    // If the trajectory angle is greater than or equal to this number, the bullet will not auto-assist towards it

    private List<TargetingData> targetData = new List<TargetingData>();    // List of targets the bullet may try to assist aiming towards
    [SerializeField]
    private Rigidbody2D rb2D;   // Rigidbody on this object
    [SerializeField]
    private string autoAimRadiusTag;    // Any object with a trigger that functions as an auto-aim radius is expected to be named with this tag

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the target is unset, and this collider is an auto aim trigger...
        if(collision.CompareTag(autoAimRadiusTag))
        {
            //...set this collider to the target
            targetData.Add(new TargetingData(collision.transform));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // See if the collision exited exists in the list of targeting data
        TargetingData toRemove = targetData.Find(x => x.target == collision.transform);

        // If a piece of targetting data matches the transform on this collider, remove it from the list
        if(toRemove != null)
        {
            targetData.Remove(toRemove);
        }
    }

    // Nudge the trajectory towards the target each frame 
    private void Update()
    {
        // If there is one target, nudge to it
        if(targetData.Count == 1)
        {
            targetData[0].CalculateTrajectoryAngle(rb2D);

            // If single target is no longer viable, remove it from the list
            if(TargetNotViable(targetData[0]))
            {
                targetData.RemoveAt(0);
            }
            // Otherwise, nudge towards it
            else
            {
                targetData[0].NudgeToTarget(rb2D, ASSIST_STRENGTH);
            }
        }
        // If there are multiple targets, use algorithm to decide which one to nudge towards
        else if(targetData.Count > 1)
        {
            ChooseTarget();
        }
    }

    // Function causes the bullet to choose a target out of all the targets in the list to nudge itself towards
    private void ChooseTarget ()
    {
        // Update trajectory angles
        foreach(TargetingData data in targetData)
        {
            data.CalculateTrajectoryAngle(rb2D);
        }

        // Remove non-viable targets from the list
        targetData.RemoveAll(TargetNotViable);

        // If there are still viable targets, sort the list and nudge towards the one with the smallest trajectory angle
        if(targetData.Count > 0)
        {
            targetData.Sort();
            targetData[0].NudgeToTarget(rb2D, ASSIST_STRENGTH);
        }
    }

    // Method returns true if the target passed in is not viable
    private bool TargetNotViable(TargetingData data)
    {
        return Mathf.Abs(data.trajectoryAngle) >= MAX_VIABLE_TARGET;   
    } 
}
