using UnityEngine;
using System;
using System.Collections.Generic;

/*
 * CLASS EnergyBottleneck
 * ----------------------
 * Serves as an intermediary between a stockpile and a set of energy sockets
 * connected to the stockpile. The bottleneck prevents multiple stock changes
 * from occuring in the same frame, choosing the most negative and most positive
 * stock changes and ignoring any others
 * ----------------------
 */ 

public class EnergyBottleneck : MonoBehaviour
{
    [SerializeField]
    private Stockpile stock;
    [SerializeField]
    private List<EnergySocket> sockets;

    // Stock changes scheduled to take place
    private List<int> scheduledStockIncreases = new List<int>();
    private List<int> scheduledStockDecreases = new List<int>();

    private void Start()
    {
        SetupEnergySockets();   
    }
    private void Update()
    {
        CheckStockChanges(scheduledStockIncreases, false);
        CheckStockChanges(scheduledStockDecreases, true);
    }
    // Add the stock change to the list of stock changes
    private void ScheduleStockChange(EnergyAbsorbedEventData data)
    {
        if(data.amountAbsorbed < 0)
        {
            scheduledStockDecreases.Add(data.amountAbsorbed);
        }
        else
        {
            scheduledStockIncreases.Add(data.amountAbsorbed);
        }
    }
    // Check to see if the schedule has elements in it. If it does,
    // resolve the stock changes in it
    private void CheckStockChanges(List<int> schedule, bool pickLeast)
    {
        if (schedule.Count == 1)
        {
            stock.ChangeStock(schedule[0]);
            schedule.Clear();
        }
        else if (schedule.Count > 1)
        {
            ResolveStockChanges(schedule, pickLeast);
        }
    }
    // Use the local function to get the cleaned-out version of the scheduled stock change
    private void ResolveStockChanges(List<int> schedule, bool pickLeast)
    {
        int stockChange;    // Amount the stock will change
        // Sort the schedule least to greatest
        schedule.Sort();
        // If we want the smallest change, grab the first element
        if(pickLeast)
        {
            stockChange = schedule[0];
        }
        // If we want the biggest element, grab the last element
        else
        {
            stockChange = schedule[schedule.Count - 1];
        }
        // Change the stock by the amount grabbed
        stock.ChangeStock(stockChange);
        // Clear out the schedule so the damage is not taken again
        schedule.Clear();
    }
    // Setup the energy sockets to schedule stock changes on the local list of stock changes
    private void SetupEnergySockets()
    {
        foreach(EnergySocket socket in sockets)
        {
            socket.energyAbsorbedEvent.action += ScheduleStockChange;
        }
    }
}
