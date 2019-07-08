using UnityEngine;
using UnityEngine.Events;

public abstract class EnergyEvent : MonoBehaviour
{
    /*
    * PUBLIC TYPEDEFS
    */

    [System.Serializable]
    public class NumericConstraintComponent : PolymorphicComponent<INumericConstraint> { };
    [System.Serializable]
    public class EnergyEventHandle : UnityEvent<EnergyEventData> { };

    /*
     * PUBLIC DATA
     */

    [SerializeField]
    [Tooltip("Constraint that the energy exchanged is passed through to see if the event will be invoked")]
    private NumericConstraintComponent constraint;
    [SerializeField]
    [Tooltip("Event invoked when the energy exchanged by the local energy source passes the constraint test")]
    private EnergyEventHandle energyEvent;

    protected void TestEnergyEvent(EnergyEventData data)
    {
        if (constraint.component.Test(data.energy))
        {
            energyEvent.Invoke(data);
        }
    }
}
