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
    private List<MonoBehaviourEvents> callingBehaviours; // List of triggers relevant to the script
    [SerializeField]
    private List<string> trackedTags;  // Only colliders with one of these tags will have the one trigger rule enforced on them 
    private List<Collider2D> callingColliders = new List<Collider2D>(); // List of colliders that can only be hit once in the lifetime of any other collider
    // List of colliders that have enetered this trigger and that have not yet been disabled
    // The list is continually checked for disabled/destroyed triggers and unmutes any that are disabled/destroyed
    private List<Collider2D> collidersToCheck = new List<Collider2D>();    

    // Events for when a collider is muted/unmuted
    public event UnityAction<Collider2D> colliderMutedEvent;
    public event UnityAction<Collider2D> colliderUnmutedEvent;

    private void Start()
    {
        foreach(MonoBehaviourEvents behaviour in callingBehaviours)
        {
            // Add the collider of each trigger event to the list
            Collider2D currentTrigger = behaviour.GetComponent<Collider2D>();
            if(currentTrigger != null)
            {
                callingColliders.Add(currentTrigger);
                behaviour.onTriggerEnter2D.action += CheckAddColliderToMute;
            }
        }
    }
    private void Update()
    {
        CheckUnmuteColliders();
    }
    // Check to see if the tag of the collider is being tracked,
    // and add it to the list if it is
    private void CheckAddColliderToMute(Collider2D collision)
    {
        if (trackedTags.Contains(collision.tag))
        {
            MuteCollider(collision, true);
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
            // Invoke the collider muted event
            if(colliderMutedEvent != null)
            {
                colliderMutedEvent(otherCollider);
            }
        }
        // If collider is being un-ignored, remove it from the list
        else
        {
            collidersToCheck.Remove(otherCollider);
            // Invoke collider unmuted event
            if(colliderUnmutedEvent != null)
            {
                colliderUnmutedEvent(otherCollider);
            }
        }
    }
}
