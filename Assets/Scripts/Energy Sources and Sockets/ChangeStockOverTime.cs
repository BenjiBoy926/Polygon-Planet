using UnityEngine;
using System.Collections;

/*
 * CLASS ChangeStockOverTime
 * -------------------------
 * Continually change the stock on the stockpile by the given amount
 * at the given rate
 * -------------------------
 */ 

public class ChangeStockOverTime : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Stock is changed over time on this stockpile")]
    private Stockpile stock;
    [SerializeField]
    [Tooltip("Number of seconds between each stock change")]
    private float timeBetweenChanges;
    [SerializeField]
    [Tooltip("Amount of change delivered to the stockpile at each interval")]
    private int change;

    // True while the stock is not ready to change
    // Once false, the stock is changed and immediately re-enabled
    private State _stockChangeNotReady;
    public State stockChangeNotReady { get { return _stockChangeNotReady; } }

    // Use this for initialization
    void Start()
    {
        _stockChangeNotReady = State.Construct(timeBetweenChanges, "StockChangeNotReady", gameObject);
        _stockChangeNotReady.Activate();

        // Change stock each time the state deactivates
        _stockChangeNotReady.stateDeactivatedEvent += ChangeStock;
    }

    private void ChangeStock()
    {
        stock.ChangeStock(change);
        _stockChangeNotReady.Activate();
    }
}
