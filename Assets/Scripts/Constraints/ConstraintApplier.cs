using UnityEngine;

/*
 * CLASS ConstraintApplier
 * -----------------------
 * Abstract base class for objects that apply constraints to 
 * constrainable objects. Base classes decide the implementation
 * of the constraint function itself
 * -----------------------
 */ 

public abstract class ConstraintApplier : MonoBehaviour
{
    /*
     * PUBLIC TYPEDEFS
     */

    [System.Serializable] public class ConstraintComponent : PolymorphicComponent<IConstrainable> { };

    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    [Tooltip("Reference to the game object with the IConstrainable component on it")]
    private ConstraintComponent constrainable;

    protected virtual void Start()
    {
        constrainable.Initialize();
        constrainable.component.constraints.Add(Constraint);
    }

    protected abstract bool Constraint();
}
