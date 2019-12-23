using UnityEngine;
using UnityEngine.Events;
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
    [Tooltip("If true, the script automatically uses this game object " +
        "and all of its immediate children as the calling behaviours")]
    private bool useChildren;

    [SerializeField]
    [Tooltip("The list of trigger event to track. Only one trigger can be " +
        "triggered in the lifetime of the object that triggers any of these triggers")]
    private List<MonoBehaviourEvents> _callingBehaviours; // List of triggers relevant to the script

    [SerializeField]
    [Tooltip("Any object with this tag will only be able to trigger " +
        "one trigger in the list of calling triggers")]
    private List<string> trackedTags;  

    private List<MonoBehaviourEvents> callingBehaviours
    {
        get
        {
            // If we're using the children for this script,
            // add all the scripts of the immediate children 
            // and return the list
            if (useChildren)
            {
                List<MonoBehaviourEvents> behaviours = new List<MonoBehaviourEvents>();

                // Add this object
                MonoBehaviourEvents current = GetComponent<MonoBehaviourEvents>();
                if (current != null)
                {
                    behaviours.Add(current);
                }

                // Add the object's immediate children
                for(int index = 0; index < transform.childCount; index++)
                {
                    current = transform.GetChild(index).GetComponent<MonoBehaviourEvents>();
                    if (current != null)
                    {
                        behaviours.Add(current);
                    }
                }

                return behaviours;
            }
            // If we're not using the children, use the editor list
            else
            {
                return _callingBehaviours;
            }
        }
    }

    private List<Collider2D> callingColliders
    {
        get
        {
            List<Collider2D> colliders = new List<Collider2D>();
            Collider2D current;

            foreach(MonoBehaviourEvents behaviour in callingBehaviours)
            {
                current = behaviour.GetComponent<Collider2D>();
                if(current != null)
                {
                    colliders.Add(current);
                }
            }

            return colliders;
        }
    }

    // List of colliders that have enetered this trigger and that have not yet been disabled
    // The list is continually checked for disabled/destroyed triggers and unmutes any that are disabled/destroyed
    private List<Collider2D> collidersToCheck = new List<Collider2D>();    

    // Events for when a collider is muted/unmuted
    public event UnityAction<Collider2D> colliderMutedEvent;
    public event UnityAction<Collider2D> colliderUnmutedEvent;

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
