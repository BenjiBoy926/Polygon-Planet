using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS SuppressSimultaneousEnergyAbsorptions
 * -------------------------------------------
 * An advanced way to connect energy sockets to stockpiles. In this class,
 * certain socket-stockpile connections are given priority over others.  
 * If two or more sockets signal an energy absoption on the same frame,
 * the one with higher priority is taken and the others are discarded
 * 
 * EXAMPLE:
 * If an object has a socket on their shield and their body, the socket
 * on the shield should be given priority over the body, since things that
 * hit the shield should not be allowed to transmit energy to the body
 * at the same time under any circumstances
 * ------------------------------------------
 */

public class SuppressSimultaneousEnergyAbsorptions : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The socket-stockpile pairs are prioritized " +
        "so that the LATEST pair has the highest priority")]
    private List<SocketStockpilePair> pairs;

    // Helper class greatly eases the task of letting only one energy
    // absorption be registered each frame
    private SimultaneousSignalSuppressor<EnergyEventData> energyAbsorbedSuppressor;

    private void Start()
    {
        // Initialize the suppressor
        energyAbsorbedSuppressor = new SimultaneousSignalSuppressor<EnergyEventData>(GetLocalPriority, ChangeAllStock);
        // Have each energy socket add the event data to the signal suppressor when it absorbs energy
        foreach (SocketStockpilePair pair in pairs)
        {
            pair.socket.energyAbsorbedEvent.AddListener(energyAbsorbedSuppressor.AddSignal);
        }
    }

    // Transmit only one signal per frame
    private void Update()
    {
        energyAbsorbedSuppressor.TransmitAndSuppress();
    }

    // When the signal suppressor transmits the signal, this method is called
    // Find all local pairs whose energy sockets match the one that sent the signal,
    // then change stock on each stockpile in each pair
    private void ChangeAllStock(EnergyEventData eventData)
    {
        List<SocketStockpilePair> matchedPairs = pairs.FindAll(x => x.socket == eventData.socket);

        foreach(SocketStockpilePair pair in matchedPairs)
        {
            pair.stockpile.ChangeStock(eventData.energy);
        }
    }

    // Find the priority wrapper whose energy socket matches the one in the event data 
    // and return the index of its appearance in the list
    private int GetLocalPriority(EnergyEventData eventData)
    {
        return pairs.IndexOf(pairs.Find(x => x.socket == eventData.socket));
    }
}
