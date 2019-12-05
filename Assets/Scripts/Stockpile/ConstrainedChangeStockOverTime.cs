using UnityEngine;
using System.Collections;

/*
 * CLASS ConstrainedChangeStockOverTime
 * ------------------------------------
 * A type of change stock over time that cannot change the stock
 * while any of the given constraints are false
 * 
 * If the constraints become false during an interval of stock
 * change, the stock change is postponed until the constraint
 * is lifted, then the stock change resumes as normal
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
            // Postpone the stock change until the constraint is lifted
            StopCoroutine("PostponeStockChange");
            StartCoroutine("PostponeStockChange");
        }
    }

    private IEnumerator PostponeStockChange()
    {
        // Get stuck in a loop while the constraint is not satisfied
        while(!constraints.result)
        {
            yield return null;
        }

        // Once the constraint is satisfied, change the stock
        base.ChangeStockAndUpdateState();
    }
}
