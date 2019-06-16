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
    [SerializeField]
    private int _amountTransferred;

    public EnergySocket socket { get { return _socket; } }
    public EnergySource source { get { return _source; } }
    public int amountTransferred { get { return _amountTransferred; } }

    public EnergyTransferredEventData(EnergySocket energySocket, EnergySource energySource, int energyTransferred)
    {
        _socket = energySocket;
        _source = energySource;
        _amountTransferred = energyTransferred;
    }
}
