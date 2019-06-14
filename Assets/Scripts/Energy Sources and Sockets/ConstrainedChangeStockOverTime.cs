using UnityEngine;
using System;

/*
 * CLASS ConstrainedChangeStockOverTime
 * ------------------------------------
 * A type of change stock over time that cannot change the stock
 * while any of the given constraints are false
 * 
 * If the constraints become false during an interval of stock
 * change, the stock change stops itself and the stock 
 * does not change
 * ------------------------------------
 */ 

public class ConstrainedChangeStockOverTime : ChangeStockOverTime, IConstrainable
{
    private BooleanFunctorList _constraints = new BooleanFunctorList();
    public BooleanFunctorList constraints { get { return _constraints; } }

    public override void StartStockChange()
    {
        if (constraints.result)
        {
            base.StartStockChange();
        }
    }

    protected override void ChangeStockAndUpdateState()
    {
        if (constraints.result)
        {
            base.ChangeStockAndUpdateState();
        }
        else
        {
            StopStockChange();
        }
    }
}
