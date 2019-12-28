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
    [Tooltip("Determine the relationship between the sockets and stockpiles. " +
        "If many to one, then all the energy sockets are connected to the same stockpile. " +
        "If not, then many energy sockets could be related to many stockpiles")]
    private SocketStockpileRelationship relationship;

    // MANY TO MANY DATA

    [SerializeField]
    [Tooltip("If true, the script tries to find a socket and a stockpile " +
        "on each of this game object's immediate children")]
    private bool useChildren;

    [SerializeField]
    [Tooltip("The socket-stockpile pairs are prioritized " +
        "so that the LATEST pair has the highest priority")]
    private List<SocketStockpilePair> pairs;

    // MANY TO ONE DATA

    [SerializeField]
    [Tooltip("If true, the script automatically uses the first Stockpile script " +
        "it can find from this game object's immediate children")]
    private bool useChildrenForStockpile;

    [SerializeField]
    [Tooltip("The one stockpile that all energy sockets are related to")]
    private Stockpile singleStockpile;

    [SerializeField]
    [Tooltip("If true, the script automatically populates the list of energy sockets " +
        "from this game object's immediate children")]
    private bool useChildrenForSockets;

    [SerializeField]
    [Tooltip("The list of energy sockets that are all related to the single stockpile")]
    private List<EnergySocket> energySockets;

    // Helper class greatly eases the task of letting only one energy
    // absorption be registered each frame
    private SimultaneousSignalSuppressor<EnergyEventData> energyAbsorbedSuppressor;

    private void Start()
    {
        Setup();
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

    // Setup the data on the script
    private void Setup()
    {
        switch(relationship)
        {
            case SocketStockpileRelationship.ManyToOne:
                SetupManyToOne();
                break;
            case SocketStockpileRelationship.ManyToMany:
                SetupManyToMany();
                break;
        }

        // Initialize the suppressor
        energyAbsorbedSuppressor = new SimultaneousSignalSuppressor<EnergyEventData>(GetLocalPriority, ChangeAllStock);

        // Have each energy socket add the event data to the signal suppressor when it absorbs energy
        foreach (SocketStockpilePair pair in pairs)
        {
            pair.socket.energyAbsorbedEvent.AddListener(energyAbsorbedSuppressor.AddSignal);
        }

    }
    private void SetupManyToMany()
    {
        if (useChildren)
        {
            pairs.Clear();

            transform.ForEachChild(child =>
            {
                // Try to get the components on the child
                EnergySocket currentSocket = child.GetComponent<EnergySocket>();
                Stockpile currentStockpile = child.GetComponent<Stockpile>();

                // If both components exist, pair them up
                if (currentSocket != null && currentStockpile != null)
                {
                    pairs.Add(new SocketStockpilePair(currentSocket, currentStockpile));
                }
            });

            if (pairs.Count <= 0)
            {
                throw new MissingComponentException("The Energy Absorption Suppressor on the game object named " +
                name + " has no children that have an EnergySocket component and a Stockpile component");
            }
        }
    }
    private void SetupManyToOne()
    {
        if(useChildrenForSockets)
        {
            energySockets.Clear();

            transform.ForEachChild(child =>
            {
                // Try to get an energy socket off of the current child and add it to the list
                EnergySocket currentSocket = child.GetComponent<EnergySocket>();
                if(currentSocket != null)
                {
                    energySockets.Add(currentSocket);
                }
            });

            if(energySockets.Count <= 0)
            {
                throw new MissingComponentException("The Energy Absorption Suppressor on the game object named " + 
                    name + " has no EnergySocket components in any of its children");
            }
        }
        
        if(useChildrenForStockpile)
        {
            singleStockpile = GetComponentInChildren<Stockpile>(true);

            if(singleStockpile == null)
            {
                throw new MissingComponentException("The Energy Absorption Suppressor on the game object named " +
                    name + " has no Stockpile components in any of its children");
            }
        }

        // Set up the pairs so that all sockets are related to the one stockpile
        pairs.Clear();
        foreach(EnergySocket socket in energySockets)
        {
            pairs.Add(new SocketStockpilePair(socket, singleStockpile));
        }
    }
}

public enum SocketStockpileRelationship
{
    ManyToMany, ManyToOne
}