using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

/*
 * CLASS Stockpile
 * ---------------
 * Generic stockpile. Change the amount in the stockpile
 * using whatever mechanism you want. Robust event systems
 * allow proper reactions to stock changing, become full of
 * being depleted. Stockpiles are great for health,
 * energy levels, and just about anything else 
 * along the same lines
 * ---------------
 */ 

public class Stockpile : MonoBehaviour, ILabelledComponent
{
    /*
     * PUBLIC TYPEDEFS
     */

    [Serializable] public class IntEvent : UnityEvent<int> { };

    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    private string _label;   // Explanatory label describing what the stock represents
    public string label { get { return _label; } }
    [SerializeField]
    private int _maxStock;  // Max energy that can be stored in the stockpile
    public int maxStock { get { return _maxStock; } }
    [SerializeField]
    private int startingStock; // Starting energy of the stockpile
    private int _currentStock; // Current energy of the stockpile

    [SerializeField]
    [Tooltip("Set of events invoked when the stock on the stockpile changes")]
    private IntEvent _stockChangedEvent;
    public IntEvent stockChangedEvent { get { return _stockChangedEvent; } }
    [SerializeField]
    [Tooltip("Set of events invoked when the stock on the stockpile is maxed out")]
    private UnityEvent _stockFilledEvent;
    public UnityEvent stockFilledEvent { get { return _stockFilledEvent; } }
    [SerializeField]
    [Tooltip("Set of events invoked when the stock on the stockpile is emptied out")]
    private UnityEvent _stockEmptiedEvent;
    public UnityEvent stockEmptiedEvent { get { return _stockEmptiedEvent; } }

    // Setting the current stock also forces it into min-max range
    // and invokes the stock changed event
    public int currentStock
    {
        get { return _currentStock; }
        protected set
        {
            int previousStock = _currentStock;
            bool previousFull = full;
            bool previousEmpty = empty;

            // Change the current stock to the valuegiven
            _currentStock = Mathf.Clamp(value, 0, _maxStock);

            // Invoke stock change event
            _stockChangedEvent.Invoke(_currentStock - previousStock);

            // If stock is now full, invoke event
            if (full && !previousFull)
            {
                _stockFilledEvent.Invoke();
            }
            // If stock is now empty, invoke event
            if (empty && !previousEmpty)
            {
                _stockEmptiedEvent.Invoke();
            }
        }
    }
    // Properties tell if stock is full or empty
    public bool full { get { return _currentStock >= _maxStock; } }
    public bool empty { get { return _currentStock <= 0f; } }

    protected virtual void Start()
    {
        _currentStock = startingStock;
    }
    // Change stock by amount specified
    public void ChangeStock(int delta)
    {
        currentStock += delta;
    }
    // Empty out the stock by setting it to zero
    public void EmptyStock()
    {
        currentStock = 0;
    }
    public void FillStock()
    {
        currentStock = _maxStock;
    }
    // Find all game objects with the given tag, then try to find a single stockpile on each game object with the tag given
    public static Stockpile[] FindStockpilesWithLabel(string gObjectTag, string stockpileLabel)
    {
        return LabelledComponentUtility.FindComponentsWithLabel<Stockpile>(gObjectTag, stockpileLabel);
    }
    // Try to find a stockpile on each of the game objects given
    public static Stockpile[] FindStockpilesWithLabel(GameObject[] gObjects, string stockpileLabel)
    {
        return LabelledComponentUtility.FindComponentsWithLabel<Stockpile>(gObjects, stockpileLabel);
    }
    // Find a game object with the given tag, then find a stockpile on that game object with the given tag
    public static Stockpile FindStockpileWithLabel(string gObjectTag, string stockpileLabel)
    {
        return LabelledComponentUtility.FindComponentWithLabel<Stockpile>(gObjectTag, stockpileLabel);
    }
    // Find a stockpile on the given game object with the given tag
    public static Stockpile FindStockpileWithLabel(GameObject gObject, string stockpileLabel)
    {
        return LabelledComponentUtility.FindComponentWithLabel<Stockpile>(gObject, stockpileLabel);   
    }
}
