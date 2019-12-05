using UnityEngine;
using UnityEngine.Events;
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
    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    [Tooltip("Stock is changed over time on this stockpile")]
    private Stockpile stock;
    [SerializeField]
    [Tooltip("Number of seconds between each stock change")]
    private float timeBetweenChanges;
    [SerializeField]
    [Tooltip("Amount of change delivered to the stockpile at each interval")]
    private int change;
    [SerializeField]
    [Tooltip("If true, the stock changing starts as soon as the scene starts")]
    private bool changeOnStart;
    [SerializeField]
    [Tooltip("Script used to check if stock change is not yet ready")]
    private State stockChangeNotReady;  // True while the stock change is not ready
    [SerializeField]
    [Tooltip("Invoked when the stock starts changing")]
    private UnityEvent changeStartedEvent;   // Invoked when the stock starts changing
    [SerializeField]
    [Tooltip("Invoked when the stock stops changing")]
    public UnityEvent changeStoppedEvent;   // Invoked when the stock stops changing

    /*
     * HELPER DATA
     */

    private bool changingStock; // True if the stock is currently changing

    /*
     * PUBLIC INTERFACE
     */ 

    public virtual void StartStockChange()
    {
        bool alreadyChangingStock = changingStock;

        changingStock = true;
        stockChangeNotReady.Activate(timeBetweenChanges);

        // If the stock was not already chaning, invoke change started event
        if (!alreadyChangingStock)
        {
            changeStartedEvent.Invoke();
        }
    }

    public void StopStockChange()
    {
        bool alreadyChangingStock = changingStock;

        changingStock = false;

        // If the stock was currently changing, invoke the event
        if (alreadyChangingStock)
        {
            changeStoppedEvent.Invoke();
        }
    }
    // Delay the stock change with a unique delay time
    public void DelayStockChange(float delayTime)
    {
        stockChangeNotReady.Activate(delayTime);
    }
    // Delay stock change based on the intrinsic time between changes
    public void DelayStockChange()
    {
        DelayStockChange(timeBetweenChanges);
    }

    /*
     * PRIVATE HELPERS
     */

    private void Start()
    {
        // Each time the state disables, change stock and update the state
        stockChangeNotReady.stateDeactivatedEvent.AddListener(ChangeStockAndUpdateState);

        if (changeOnStart)
        {
            StartStockChange();
        }
    }

    protected virtual void ChangeStockAndUpdateState()
    {
        if(changingStock)
        {
            stock.ChangeStock(change);
            stockChangeNotReady.Activate(timeBetweenChanges);
        }
    }
}
