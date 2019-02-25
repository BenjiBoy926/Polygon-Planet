using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS OneTriggerPerLifetime
 * ---------------------------
 * This class prevents any colliders from entering the triggers of this
 * object and any child objects multiple times in that objects lifetime
 * If the other trigger is ever disabled, it can enter this trigger again
 * ---------------------------
 */ 

public class OneTriggerPerLifetime : MonoBehaviour
{
    private List<Collider2D> childColliders; // List of colliders that can only be hit once in the lifetime of any other collider
    private List<Collider2D> triggeredColliders;    // List of colliders that have enetered this trigger

    private void Start()
    {
        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        childColliders = colliders.ToList();
    }
    private void Update()
    {
        CheckUnmuteColliders();
    }
    // When any collider enters the trigger, mute it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MuteCollider(collision, true);
    }
    private void CheckUnmuteColliders()
    {
        int index = 0;
        // Remove any colliders that were destroyed
        triggeredColliders.RemoveAll(x => x.gameObject == null);
        // Loop through the list of triggered colliders
        while(index < triggeredColliders.Count)
        {
            // If the current collider is no longer active...
            if(!triggeredColliders[index].gameObject.activeInHierarchy)
            {
                //...unmute it
                MuteCollider(triggeredColliders[index], false);
            }
            // If current collider is still active, check the next one
            else
            {
                index++;
            }
        }
    }
    private void MuteCollider(Collider2D otherCollider, bool mute)
    {
        // Cause the collider to be ignored (or not ignored) by all child colliders
        foreach(Collider2D collider in childColliders)
        {
            Physics2D.IgnoreCollision(collider, otherCollider, mute);
        }
        // If collider is being muted, add it to the list
        if(mute)
        {
            triggeredColliders.Add(otherCollider);
        }
        // If collider is being un-ignored, remove it from the list
        else
        {
            triggeredColliders.Remove(otherCollider);
        }
    }
}
