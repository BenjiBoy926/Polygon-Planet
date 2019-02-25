using UnityEngine;
using System.Collections;

/*
 * CLASS DiminishingEnergySource
 * -----------------------------
 * Assigns a persistence rating to an energy source that decreases
 * whenever it transfers its energy to an energy socket
 * -----------------------------
 */ 

public class DiminishEnergySource : MonoBehaviour
{
    [SerializeField]
    private EnergySource source;    // Energy source that diminishes when it transfers its energy
    [SerializeField]
    private int maxPersistence;
    private int currentPersistence;

    // Use this for initialization
    void Start()
    {
        currentPersistence = maxPersistence;
        source.energyTransferredEvent += DecreasePersistence;
    }

    // Decrease persistence. Subscribed to energy transferred event on the energy source given
    private void DecreasePersistence(EnergySocket socket)
    {
        currentPersistence--;
    }
}
