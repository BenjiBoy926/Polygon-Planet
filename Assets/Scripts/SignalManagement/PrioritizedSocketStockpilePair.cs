using UnityEngine;
using System.Collections;

[System.Serializable]
public class PrioritizedSocketStockpilePair
{
    [SerializeField]
    private SocketStockpilePair _pair;
    [SerializeField]
    private int _priority;

    public SocketStockpilePair pair { get { return _pair; } }
    public EnergySocket socket { get { return _pair.socket; } }
    public Stockpile stockpile { get { return _pair.stockpile; } }
    public int priority { get { return _priority; } }
}
