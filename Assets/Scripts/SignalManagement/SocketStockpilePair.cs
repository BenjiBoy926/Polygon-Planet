using UnityEngine;

[System.Serializable]
public class SocketStockpilePair
{
    [SerializeField]
    private EnergySocket _socket;
    [SerializeField]
    private Stockpile _stockpile;

    public EnergySocket socket { get { return _socket; } }
    public Stockpile stockpile { get { return _stockpile; } }
}
