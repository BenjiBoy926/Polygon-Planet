using UnityEngine;

/*
 * CLASS SocketStockpileConnection
 * -------------------------------
 * Causes the energy absorbed in an energy socket to change the stock
 * in a stockpile
 * -------------------------------
 */ 
 [System.Serializable]
public class SocketStockpileConnection
{
    [SerializeField]
    private Stockpile _stock;
    [SerializeField]
    private EnergySocket _socket;

    public Stockpile stock { get { return _stock; } }
    public EnergySocket socket { get { return _socket; } }

    private bool connected = false;
    public void Connect()
    {
        // Prevent client code from creating multiple connections
        if(!connected)
        {
            _socket.energyAbsorbedEvent.AddListener(ChangeStockByAmountAbsorbed);
            connected = true;
        }
    }
    private void ChangeStockByAmountAbsorbed(EnergyEventData data)
    {
        _stock.ChangeStock(data.energy);
    }
}
