using UnityEngine;

public class StockConstraint : ConstraintApplier
{
    [SerializeField]
    [Tooltip("The stockpile that will constrain the constrainable object")]
    private Stockpile stock;
    [SerializeField]
    [Tooltip("Defines the rules for how the constrainable object is constrained")]
    private UnaryOrBinaryNumericConstraint constraint;

    protected override bool Constraint()
    {
        return constraint.Test(stock.currentStock);
    }
}
