using UnityEngine;

public class EnergySocketEvent : EnergyEvent
{
    [SerializeField]
    [Tooltip("Energy socket that triggers the energy test")]
    private EnergySocket socket;

    // Use this for initialization
    void Start()
    {
        socket.energyAbsorbedEvent.AddListener(TestEnergyEvent);
    }
}
