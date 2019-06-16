using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS SocketStockpileConnector
 * ------------------------------
 * Simple class allows references to sockets and stockpiles to be set,
 * then connects each pair specified
 * ------------------------------
 */ 

public class SocketStockpileConnector : MonoBehaviour
{
    [SerializeField]
    private List<SocketStockpileConnection> connections;
    private void Start()
    {
        foreach(SocketStockpileConnection connection in connections)
        {
            connection.Connect();
        }
    }
}
