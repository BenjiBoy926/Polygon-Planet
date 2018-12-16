using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines a delegate type for functions that require no parameters to generate an x-y coordinate
public delegate Vector2 Director2D();

/*
 * CLASS AutoEmitter2D : Emitter2D
 * -------------------------------
 * A type of emitter that allows a calling method to specify a rule by which
 * the emitter continually shoots automatically
 * -------------------------------
 */ 

public class AutoEmitter2D : Emitter2D
{
    [SerializeField]
    private float fireRateDifference;

    public void AutoShoot (Director2D coordinates)
    {
        StartCoroutine("AutoShootIterator", coordinates);
    }

    IEnumerator AutoShootIterator (Director2D coordinates)
    {
        float delay;    // Delay between each shot of the enemy's gun

        // Store min-max times for how long it takes the gun to fire
        float minTime = primaryState.duration - fireRateDifference;
        float maxTime = primaryState.duration + fireRateDifference;

        while (true)
        {
            delay = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(delay);
            ForceEmit(coordinates());
        }
    }
}
