using UnityEngine;

public class StockConstraint : ConstraintApplier
{
    [System.Serializable] public class NumericConstraint : PolymorphicComponent<INumericConstraint> { };

    [SerializeField]
    [Tooltip("The stockpile that will constrain the constrainable object")]
    private Stockpile stock;
    [SerializeField]
    [Tooltip("Defines the rules for how the constrainable object is constrained")]
    private NumericConstraint constraint;

    protected override void Start()
    {
        base.Start();
        constraint.Initialize();
    }

    protected override bool Constraint()
    {
        return constraint.component.Test(stock.currentStock);
    }
}
