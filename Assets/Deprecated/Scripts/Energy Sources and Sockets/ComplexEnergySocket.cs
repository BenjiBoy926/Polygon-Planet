using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

/*
 * CLASS ComplexEnergySocket : MonoBehaviour
 * -----------------------------------------
 * Defines an energy socket capable of recieving energy absorbtion inputs through an
 * aggregation of multiple energy sockets.  The script strictly
 * enforces a "one source, one absorption" rule that prevents a single energy source
 * from transferring its energy to multiple partial energy sockets.
 * -----------------------------------------
 */

public class ComplexEnergySocket : MonoBehaviour, IEnergySocketInfoHandler
{
    [SerializeField]
    private EnergySocketInfo socketInfo;    // Info on the current energy levels of the socket
    public EnergySocketInfo info { get { return socketInfo; } }

    private List<PartialEnergySocket> parts = new List<PartialEnergySocket>();  // Parts on the complex that take in energy from hazards  
    private List<Energy> scheduledEnergyDecrease = new List<Energy>();  // List of energies scheduled to DEcrease the energy of this complex
    private List<Energy> scheduledEnergyIncrease = new List<Energy>();  // List of energies scheduled to INcrease the energy of this complex
    private List<Collider2D> sourcesHit = new List<Collider2D>();    // List of colliders belonging to each object that has hurt this complex in its lifetime

    private void Start()
    {
        socketInfo.Initialize();
    }
    private void OnEnable()
    {
        socketInfo.Initialize();
    }
    private void Update()
    {
        CheckSchedule(scheduledEnergyDecrease, socketInfo.DecreaseEnergy);
        CheckSchedule(scheduledEnergyIncrease, socketInfo.IncreaseEnergy);
        CheckUnmute();
    }    
    // Schedule the health complex to take damage from the given energy
    public void ScheduleEnergyDecrease(Energy sourceInfo)
    {
        scheduledEnergyDecrease.Add(sourceInfo);
    }
    public void ScheduleEnergyIncrease(Energy sourceInfo)
    {
        scheduledEnergyIncrease.Add(sourceInfo);
    }
    // Check the schedule specified and 
    private void CheckSchedule(List<Energy> schedule, UnityAction<Energy> response)
    {
        // If there's only one item in the schedule, invoke the response for that one object
        if (schedule.Count == 1)
        {
            response(schedule[0]);
            schedule.RemoveAt(0);
        }
        // If there are many items scheduled, 
        // we need the more performance-intensive function to sort out the energy absorption
        else if (schedule.Count > 1)
        {
            AbsorbAllEnergies(schedule, response);
        }
    }
    // Cleans the schedule given of repeated hits from the same energy source,
    // and absorbes the energy as specified by the calling method
    private void AbsorbAllEnergies(List<Energy> schedule, UnityAction<Energy> response)
    {
        int index = 0;

        // Clean the schedule by making sure only one instance of any given collider
        // exists only once in the list
        while (index < schedule.Count)
        {
            //CleanSchedule(schedule, schedule[index].hitbox);
            ++index;
        }

        // Once cleaned, absorb all energy specified in the schedule
        foreach (Energy energy in schedule)
        {
            response(energy);
        }

        // Clear all damage scheduled
        schedule.Clear();
    }
    // Given a collider, the method searches the schedule of energy info and removes
    // all but the most powerful energy belonging to that collider
    private void CleanSchedule(List<Energy> schedule, Collider2D search)
    {
        // Predicate returns true if local collider matches the one on the current energy source info
        //Predicate<Energy> MatchColliders = x => x.hitbox == search;
        //// Find all scheduled damage instances that have a reference to the same collider passed in as a parameter
        //List<Energy> matches = schedule.FindAll(MatchColliders);

        //// Enter selection if multiple damages have the same collider
        //// (i.e. if multiple damages were scheduled by the same hazardous object on multiple damageable parts)
        //if (matches.Count > 1)
        //{
        //    // Sort the list from least to greates damage, and remove the greatest damage
        //    matches.Sort();
        //    matches.RemoveAt(matches.Count - 1);

        //    // Remove all but the highest damage info from scheduled damage
        //    foreach (Energy info in matches)
        //    {
        //        schedule.Remove(info);
        //    }
        //}
    }
    // Prevent the given collider from colliding with any damageable object associated with this script
    private void IgnoreEnergySource(Collider2D col, bool muted)
    {
        foreach (IDamageable2D part in parts)
        {
            Physics2D.IgnoreCollision(part.hitBox, col, muted);
        }

        // If the collider is being ignored...
        if (muted)
        {
            //...add it to the list of offenders to check later
            sourcesHit.Add(col);
        }
        // If this collider's collisions are being re-enabled...
        else
        {
            //...remove it from the list
            sourcesHit.Remove(col);
        }
    }
    // Check the sources that have hit this complex and unmute any that have been disabled
    private void CheckUnmute()
    {
        if (sourcesHit.Count == 1)
        {
            // If the offender is no longer active, re-enable collisions with it
            if (!sourcesHit[0].gameObject.activeInHierarchy)
            {
                IgnoreEnergySource(sourcesHit[0], false);
            }
        }
        else if (sourcesHit.Count > 1)
        {
            int index = 0;

            // Loop through the list of offenders
            while (index < sourcesHit.Count)
            {
                // If the current offender is no longer active, re-enable collisions with it
                if (!sourcesHit[index].gameObject.activeInHierarchy)
                {
                    IgnoreEnergySource(sourcesHit[index], false);
                }
                else
                {
                    index++;
                }
            }
        }
    }
    // Add given partial socket to list
    public void RegisterSocket(PartialEnergySocket register)
    {
        parts.Add(register);
    }
}
