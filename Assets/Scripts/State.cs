using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * CLASS State
 * -----------
 * Highly useful class makes keeping track of the state an object easy
 * States can be activated for a set amount of time or locked into the active/inactive
 * state. Robust event systems allow client code to have their methods called as soon
 * as the state is activated/deactivated
 * -----------
 */ 

[System.Serializable]
public class State
{
    public const float LOCKED_ACTIVATION_DURATION = -1f;    // This floating-point value is sent to the activated event if the state is locked into the activated state
    private const int THREAD_SLEEP_INTERVAL = 10;   // Interval in milliseconds that the disable thread sleeps

    [SerializeField]
    private float _duration; // Default duration the state lasts when activated
    private float timer;    // Timer used to determine if the state is active in time

    private bool isLocked;  // True if the state has been locked into true or false
    private bool lockedState;   // The state if it is being locked

    // Event handling: class has an activated and deactivated event
    // Uses threading to call the deactivate event
    private UnityAction<float> onStateActivated;   // Multicast function pointer is called whenever the state activates
    private UnityAction onStateDeactivated; // Multicast funciton pointer is called as soon as the state is deactivated
    private Thread deactivateThread;    // Thread used to execute the disable method as soon as the state deactivates

    public float duration { get { return _duration; } }

    public State()
    {
        _duration = 1f;
    }
    public State (float d)
    {
        _duration = d;
    }

    // When destroyed, interrupt any threads that are sleeping
    ~State() { deactivateThread.Interrupt(); }

    // State's implicitly converted to bool return true while active and false while inactive
    public static implicit operator bool(State state)
    {
        // If not locked, choose time comparison. If locked, choosed locked state
        return (Time.time < state.timer && !state.isLocked) || (state.lockedState && state.isLocked);
    }

    // Activate the state by setting the timer to current time plus local duration
    public void Activate ()
    {
        if(!isLocked)
        {
            timer = Time.time + duration;
            DeactivateAfterTime(duration);

            // Check before invoking the event
            if (onStateActivated != null)
            {
                onStateActivated(duration);
            } // endif not null
        } // endif not locked
    }
    // Overload of the Activate () method allows calling method to specify a custom duration for the state
    public void Activate (float customDuration)
    {
        if(!isLocked)
        {
            timer = Time.time + customDuration;
            DeactivateAfterTime(customDuration);

            // Check before invoking the event
            if (onStateActivated != null)
            {
                onStateActivated(customDuration);
            } // endif not null
        } // endif not locked
    }
    // Disable the state by setting the timer to an invalid number
    public void Deactivate()
    {
        if(!isLocked)
        {
            timer = -1f;

            // Interrupt any deactivation threads that might currently be running
            if(deactivateThread != null)
            {
                deactivateThread.Interrupt();
            }

            // Check and invoke the event
            if (onStateDeactivated != null)
            {
                onStateDeactivated();
            } // endif not null
        } // endif not locked
    }

    // Lock the state to the bool specified
    // It cannot change states again until "Unlock" is called
    public void Lock (bool state)
    {
        // Set locked to true and locked state to the state specified
        isLocked = true;
        lockedState = state;

        // Make sure any deactivate thread still sleeping is interrupted
        timer = -1f;
        deactivateThread.Interrupt();

        // Call state activated event if it was locked to true
        if(onStateActivated != null && state)
        {
            onStateActivated(LOCKED_ACTIVATION_DURATION);
        }
        // Call state deactivated event if it was locked to false
        else if(onStateDeactivated != null && !state)
        {
            onStateDeactivated();
        }
    }
    public void Unlock()
    {
        isLocked = false;
    }

    // Add/remove the method specified to the activation event
    public void AddActivatedEvent(UnityAction<float> method)
    {
        onStateActivated += method;
    }
    public void RemoveActivatedEvent(UnityAction<float> method)
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
        float totalTime = (float)time;
        float activeTime = 0f;

        // Loop until the thread has been active for the total amount of time
        while(activeTime < totalTime)
        {
            Thread.Sleep(THREAD_SLEEP_INTERVAL);
            activeTime += THREAD_SLEEP_INTERVAL * 0.001f * Timekeeper.timeScale;
        }

        Deactivate();
    }
}
