using System;
using System.Collections.Generic;
using UnityEngine;

public class SimultaneousSignalSuppressor<TSignal>
{
    // List of the signals pending for transmission,
    // sorted based on a priority number assigned to each
    private List<PriorityWrapper<TSignal>> pendingSignals;
    // Function pointer that assigns a priority to the
    // given signal type
    private Func<TSignal, int> priorityGenerator;
    // Function pointer called when a signal is transmitted
    private event UnityAction<TSignal> signalTransmissionEvent;

    public SimultaneousSignalSuppressor(Func<TSignal, int> priotiryFunction, UnityAction<TSignal> transmissionEvent)
    {
        pendingSignals = new List<PriorityWrapper<TSignal>>();
        priorityGenerator = priotiryFunction;
        signalTransmissionEvent = transmissionEvent;
    }
    // Add a signal to the list of pending signals
    public void AddSignal(TSignal signal)
    {
        pendingSignals.Add(new PriorityWrapper<TSignal>(signal, priorityGenerator(signal)));
        Debug.Log("Added signal " + signal + " with priority " + priorityGenerator(signal));
    }
    // Transmit the signal with the highest priority and 
    // delete all other pending signals
    public void TransmitAndSuppress()
    {
        if(pendingSignals.Count == 1)
        {
            signalTransmissionEvent(pendingSignals[0].data);
            pendingSignals.RemoveAt(0);
        }
        else if(pendingSignals.Count > 1)
        {
            pendingSignals.Sort();
            signalTransmissionEvent(pendingSignals[pendingSignals.Count - 1].data);
            Debug.Log("Transmitting signal " + pendingSignals[pendingSignals.Count - 1]);
            pendingSignals.Clear();
        }
    }
}
