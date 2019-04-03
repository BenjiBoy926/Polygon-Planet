using UnityEngine;

// Simple class encapsulates data given when an energy source
// transfers energy to an energy socket
[System.Serializable]
public class EnergyTransferredEventData
{
    [SerializeField]
    private EnergySocket _socket;
    [SerializeField]
    private EnergySource _source;

    public EnergySocket socket { get { return _socket; } }
    public EnergySource source { get { return _source; } }

    public EnergyTransferredEventData(EnergySocket energySocket, EnergySource energySource)
    {
        _socket = energySocket;
        _source = energySource;
    }
}
