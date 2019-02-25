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
    private Stockpile stock;
    [SerializeField]
    private EnergySocket socket;
    private bool connected = false;
    public void Connect()
    {
        // Prevent client code from creating multiple connections
        if(!connected)
        {
            socket.energyAbsorbedEvent += stock.ChangeStock;
            connected = true;
        }
    }
}
