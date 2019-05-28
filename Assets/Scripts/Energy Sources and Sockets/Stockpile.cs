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
    [SerializeField]
    private string _label;   // Explanatory label describing what the stock represents
    public string label { get { return _label; } }
    [SerializeField]
    private int maxStock;  // Max energy that can be stored in the stockpile
    [SerializeField]
    private int startingStock; // Starting energy of the stockpile
    private int _currentStock; // Current energy of the stockpile

    // Events
    public event UnityAction<int> stockChangedEvent;
    public event UnityAction stockFilledEvent;
    public event UnityAction stockEmptiedEvent;

    // Setting the current stock also forces it into min-max range
    // and invokes the stock changed event
    public int currentStock
    {
        get { return _currentStock; }
        protected set
        {
            int previousStock = _currentStock;
            _currentStock = Mathf.Clamp(value, 0, maxStock);

            // Invoke stock changed event
            if(stockChangedEvent != null)
            {
                stockChangedEvent(_currentStock - previousStock);
            }
            // If stock is now full, invoke event
            if (full && stockFilledEvent != null)
            {
                stockFilledEvent();
            }
            // If stock is now empty, invoke event
            if (empty && stockEmptiedEvent != null)
            {
                stockEmptiedEvent();
            }
        }
    }
    // Properties tell if stock is full or empty
    public bool full { get { return _currentStock >= maxStock; } }
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
    // Find all game objects with the given tag, then try to find a single stockpile on each game object with the tag given
    public static Stockpile[] FindStockpilesWithLabel(string gObjectTag, string stockpileLabel)
    {
        return ComponentUtility.FindComponentsWithLabel<Stockpile>(gObjectTag, stockpileLabel);
    }
    // Try to find a stockpile on each of the game objects given
    public static Stockpile[] FindStockpilesWithLabel(GameObject[] gObjects, string stockpileLabel)
    {
        return ComponentUtility.FindComponentsWithLabel<Stockpile>(gObjects, stockpileLabel);
    }
    // Find a game object with the given tag, then find a stockpile on that game object with the given tag
    public static Stockpile FindStockpileWithLabel(string gObjectTag, string stockpileLabel)
    {
        return ComponentUtility.FindComponentWithLabel<Stockpile>(gObjectTag, stockpileLabel);
    }
    // Find a stockpile on the given game object with the given tag
    public static Stockpile FindStockpileWithLabel(GameObject gObject, string stockpileLabel)
    {
        return ComponentUtility.FindComponentWithLabel<Stockpile>(gObject, stockpileLabel);   
    }
}
