using UnityEngine;
using System.Collections;

/*
 * CLASS VolatileEnergySocketEffector
 * ----------------------------------
 * Activates a particle effect whenever the energy socket increases/
 * decreases energy. Neither effect is disabled, so the effect is 
 * expected to disable itself (is "volatile")
 * ----------------------------------
 */

public class VolatileEnergySocketEffector : MonoBehaviour
{
    [SerializeField]
    private Transform energySocket;    // Object expected to have an energy socket component on it
    [SerializeField]
    private ParticleEffect energyIncreasedEffect;   // Effect released when energy on the socket is increased
    [SerializeField]
    private ParticleEffect energyDecreasedEffect;   // Effect released when energy on the socket is decreased

    private void Start()
    {
        // Add local methods to the events on the energy socket
        EnergySocket socket = energySocket.GetComponentInChildren<EnergySocket>();
        if(socket != null)
        {
            //socket.energyIncreasedEvent += EnergyIncreasedEffect;
            //socket.energyDecreasedEvent += EnergyDecreasedEffect;
        }
        // Initialize the particle effects
        energyIncreasedEffect.Initialize();
        energyDecreasedEffect.Initialize();
    }
    // Enable effect at the position of the energy socket
    private void EnergyIncreasedEffect(Energy info)
    {
        energyIncreasedEffect.EnableEffect(energySocket.position);
    }
    private void EnergyDecreasedEffect(Energy info)
    {
        energyDecreasedEffect.EnableEffect(energySocket.position);
    }
}
