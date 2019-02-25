using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS OneTriggerPerLifetime
 * ---------------------------
 * This class prevents any colliders from entering the triggers specified
 * multiple times in that objects lifetime
 * If the other trigger is ever disabled, it can enter this trigger again
 * Only colliders with one of the checked tags are muted
 * ---------------------------
 */ 

public class OneTriggerPerLifetime : MonoBehaviour
{
    [SerializeField]
    private List<TriggerEvent> callingTriggers; // List of triggers relevant to the script
    [SerializeField]
    private List<string> trackedTags;  // Only colliders with one of these tags will have the one trigger rule enforced on them 
    private List<Collider2D> callingColliders = new List<Collider2D>(); // List of colliders that can only be hit once in the lifetime of any other collider
    // List of colliders that have enetered this trigger and that have not yet been disabled
    // The list is continually checked for disabled/destroyed triggers and unmutes any that are disabled/destroyed
    private List<Collider2D> collidersToCheck = new List<Collider2D>();    
    private List<Collider2D> collidersToMute = new List<Collider2D>();  // Colliders that will be muted at the end of the next frame

    private void Start()
    {
        foreach(TriggerEvent trigger in callingTriggers)
        {
            // Add the collider of each trigger event to th list
            Collider2D currentTrigger = trigger.GetComponent<Collider2D>();
            if(currentTrigger != null)
            {
                callingColliders.Add(currentTrigger);
            }
            // Check any collider entering the trigger event handler's trigger
            trigger.triggerEnteredEvent += CheckAddColliderToMute;
        }
    }
    private void Update()
    {
        CheckUnmuteColliders();
    }
    private void LateUpdate()
    {
        CheckMuteColliders();
    }
    // Check to see if the tag of the collider is being tracked,
    // and add it to the list if it is
    private void CheckAddColliderToMute(Collider2D collision)
    {
        if (trackedTags.Contains(collision.tag))
        {
            collidersToMute.Add(collision);
        }
    }
    private void CheckUnmuteColliders()
    {
        int index = 0;
        // Remove any colliders that were destroyed
        collidersToCheck.RemoveAll(x => x.gameObject == null);
        // Loop through the list of triggered colliders
        while(index < collidersToCheck.Count)
        {
            // If the current collider is no longer active, unmute it
            if(!collidersToCheck[index].gameObject.activeInHierarchy)
            {
                MuteCollider(collidersToCheck[index], false);
            }
            // If current collider is still active, check the next one
            else
            {
                index++;
            }
        }
    }
    // Check the list of colliders to mute, and mute any that need muting
    private void CheckMuteColliders()
    {
        if(collidersToMute.Count > 0)
        {
            // Mute colliders and clear out the list
            foreach(Collider2D col in collidersToMute)
            {
                MuteCollider(col, true);
            }
            collidersToMute.Clear();
        }
    }
    private void MuteCollider(Collider2D otherCollider, bool mute)
    {
        // Cause the collider to be ignored (or not ignored) by all child colliders
        foreach(Collider2D collider in callingColliders)
        {
            Physics2D.IgnoreCollision(collider, otherCollider, mute);
        }
        // If collider is being muted, add it to the list
        if(mute)
        {
            collidersToCheck.Add(otherCollider);
        }
        // If collider is being un-ignored, remove it from the list
        else
        {
            collidersToCheck.Remove(otherCollider);
        }
    }
}
