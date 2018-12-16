using System.Threading;
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
    private Thread deactivateThread;    // Thread used to execute the disable method as soon as the state deactivates

    public float duration { get { return _duration; } }

    public State (float d)
    {
        _duration = d;
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
        DeactivateAfterTime(duration);

        // Check before invoking the event
        if(onStateActivated != null)
        {
            onStateActivated(duration);
        }
    }
    // Overload of the Activate () method allows calling method to specify a custom duration for the state
    public void Activate (float customDuration)
    {
        timer = Time.time + customDuration;
        DeactivateAfterTime(customDuration);

        // Check before invoking the event
        if(onStateActivated != null)
        {
            onStateActivated(customDuration);
        }
    }
    // Disable the state by setting the timer to an invalid number
    public void Deactivate()
    {
        timer = -1f;
        deactivateThread.Interrupt();

        // Check and invoke the event
        if (onStateDeactivated != null)
        {
            onStateDeactivated();
        }
    }
    // Add/remove the method specified to the activation event
    public void AddActivatedEvent(StateActivatedEvent method)
    {
        onStateActivated += method;
    }
    public void RemoveActivatedEvent(StateActivatedEvent method)
    {
        onStateActivated -= method;
    }
    // Add/remove the method specified to the deactivation event
    public void AddDeactivatedEvent(UnityAction method)
    {
        onStateDeactivated += method;
    }
    public void RemoveDeactivatedEvent(UnityAction method)
    {
        onStateDeactivated -= method;
    }

    // Start a thread that will deactivate the state after the time specified
    private void DeactivateAfterTime(float timeout)
    {
        // Interrupt the current deactivation thread and start a new one
        if(deactivateThread != null)
        {
            deactivateThread.Interrupt();
        }
        
        deactivateThread = new Thread(DeactivateThread);
        deactivateThread.Start(timeout);
    }

    // Cause the current thread to sleep for the amount of time specified, then deactivate the state
    private void DeactivateThread(object time)
    {
        Thread.Sleep((int)((float)time * 1000f));
        Deactivate();
    }
}
