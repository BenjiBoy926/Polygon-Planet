using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Delegate returns void and takes as an argument the duration for which the state is set
public delegate void StateActivatedEvent(float duration);

/*
 * CLASS State
 * -----------
 * Small structure groups major operations and properties that allows
 * a state to be checked at runtime
 * -----------
 */ 

[System.Serializable]
public class State
{
    [SerializeField]
    private float _duration; // Default duration the state lasts when activated
    private float timer;    // Timer used to determine if the state is active in local property
    private StateActivatedEvent onStateActivated;   // Multicast function pointer is called whenever the state activates
    private UnityAction onStateDeactivated; // Multicast funciton pointer is called as soon as the state is deactivated

    public float duration { get { return _duration; } }

    public State (float d)
    {
        _duration = d;
    }
    public State (float d, StateActivatedEvent initial)
    {
        _duration = d;
        onStateActivated = initial;
    }

    // State's implicitly converted to bool return true while active and false while inactive
    public static implicit operator bool(State s)
    {
        return Time.time < s.timer;
    }

    // Activate the state by setting the timer to current time plus local duration
    public void Activate ()
    {
        timer = Time.time + duration;        

        // Check before invoking the event
        if(onStateActivated != null)
        {
            onStateActivated(duration);
        }
    }
    // Takes in a float that is not used. This function fulfills the same contract with StateActivatedEvent delegate type but simply uses the local duration
    public void ActivateDummy(float dummyVar)
    {
        Activate();
    }
    // Overload of the Activate () method allows calling method to specify a custom duration for the state
    public void Activate (float customDuration)
    {
        timer = Time.time + customDuration;

        // Check before invoking the event
        if(onStateActivated != null)
        {
            onStateActivated(customDuration);
        }
    }
    // Effectively disable the state by setting the timer to an invalid number
    public void Deactivate()
    {
        timer = -1f;

        // Check and invoke the event
        if (onStateDeactivated != null)
        {
            onStateDeactivated();
        }
    }
    // Add the method specified to the activation event
    public void AddActivatedEvent(StateActivatedEvent method)
    {
        onStateActivated += method;
    }
    // Add the method specified to the deactivation event
    public void AddDeactivatedEvent(UnityAction method)
    {
        onStateDeactivated += method;
    }
}
