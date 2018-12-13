using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float duration; // Default duration the state lasts when activated
    private float timer;    // Timer used to determine if the state is active in local property

    // Activate the state by setting the timer to current time plus local duration
    public void Activate ()
    {
        timer = Time.time + duration;
    }
    // Overload of the Activate () method allows calling method to specify a custom duration for the state
    public void Activate (float customDuration)
    {
        timer = Time.time + customDuration;
    }
    // Effectively disable the state by setting the timer to an invalid number
    public void Deactivate ()
    {
        timer = -1f;
    }
    // State's implicitly converted to bool return true while active and false while inactive
    public static implicit operator bool(State s)
    {
        return Time.time < s.timer;
    }
}
