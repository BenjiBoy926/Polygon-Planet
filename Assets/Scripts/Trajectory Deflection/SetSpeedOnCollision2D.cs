using UnityEngine;

public class SetSpeedOnCollision2D : CollisionComponentProcessor<Rigidbody2D>
{
    /*
     * PUBLIC TYPEDEFS
     */ 
    public enum SetOrScale { Set, Scale }

    [SerializeField]
    [Tooltip("Magnitude to set the speed to")]
    private float speed;
    [SerializeField]
    [Tooltip("Determine if the velocity of the " +
        "object collided with will have its speed directly " +
        "set to the given value or scaled")]
    private SetOrScale type;

    protected override void ProcessComponent(Rigidbody2D component)
    {
        if (type == SetOrScale.Scale)
        {
            component.velocity *= speed;
        }
        else
        {
            component.velocity = component.velocity.ScaledVector(speed);
        }
    }
}
