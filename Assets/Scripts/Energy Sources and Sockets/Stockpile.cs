using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

/*
 * CLASS Stockpile
 * ---------------
 * Generic stockpile. Change the amount in the stockpile
 * using whatever mechanism you want. Robust event systems
 * allow proper reactions. Stockpiles are great for health,
 * energy levels, and just about anything else along the same lines
 * ---------------
 */ 

public class Stockpile : MonoBehaviour
{
    [SerializeField]
    private int maxStock;  // Max energy that can be stored in the stockpile
    [SerializeField]
    private int startingStock; // Starting energy of the stockpile
    private int _currentStock; // Current energy of the stockpile

    // Events
    public event UnityAction<int> stockChangedEvent;
    public event UnityAction stockFilledEvent;
    public event UnityAction stockEmptiedEvent;

    // Setting the current energy also forces it into min-max range
    private int currentStock
    {
        get { return _currentStock; }
        set
        {
            _currentStock = value;
            _currentStock = (int)Mathf.Clamp(_currentStock, 0f, maxStock);
        }
    }
    // Properties tell if stock is full or empty
    public bool full { get { return _currentStock >= maxStock; } }
    public bool empty { get { return _currentStock <= 0f; } }

    protected virtual void Start()
    {
        currentStock = startingStock;
    }
    // Change stock by amount specified
    public void ChangeStock(int delta)
    {
        currentStock += delta;
        // If stock changed event exists, invoke it
        if(stockChangedEvent != null)
        {
            stockChangedEvent(delta);
        }
        // If stock is now full, invoke event
        if(full && stockFilledEvent != null)
        {
            stockFilledEvent();
        }
        // If stock is now empty, invoke event
        if(empty && stockEmptiedEvent != null)
        {
            stockEmptiedEvent();
        }
        Debug.Log("Stock on " + transform.root.gameObject.name + " changed by " + delta + ". Current stock: " + currentStock);
    }
}
