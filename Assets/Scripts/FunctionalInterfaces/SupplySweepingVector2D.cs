using UnityEngine;
using System.Collections;

public abstract class SupplySweepingVector2D : MonoBehaviour, ISupplier<Vector2>
{
    /*
     * PUBLIC TYPEDEFS
     */ 

    public enum DirectionalType
    {
        Clockwise,
        CounterClockwise,
        PingPongSingleEdge,
        PingPongDoubleEdge
    }

    /*
     * PUBLIC DATA
     */

    [SerializeField]
    [Tooltip("The number of vectors supplied before reversing direction, or starting over again")]
    private int intervals;
    [SerializeField]
    [Tooltip("Range on either side of the given vector that the supplied vector sweeos")]
    private float differenceAngle;
    [SerializeField]
    [Tooltip("Determines how the supplied vector sweeps around the base vector. " +
        "It can sweep through and repeat from the beginning, clockwise or counter-clockwise," +
        "or ping-pong back and forth")]
    private DirectionalType directionalType;
    
    /*
     * PRIVATE DATA
     */ 

    private float currentAngle;
    private float angleInterval;    // Interval between each of the vectors supplied
    private int directionalConstant;    // + or -1, depending on whether the vector is sweeping clockwise or counter-clockwise

    /*
     * PUBLIC INTERFACE
     */ 

    public Vector2 Supply()
    {
        Vector2 vector = BaseVector().RotatedVector(currentAngle);
        UpdateCurrentAngle();
        return vector;
    }

    /*
     * PRIVATE HELPERS
     */ 

    protected virtual void Start()
    {
        differenceAngle = Mathf.Abs(differenceAngle);
        angleInterval = differenceAngle * 2f / (intervals - 1);

        // Initialize the directional constant so the supplied vector
        // sweeps in the correct direction
        switch (directionalType)
        {
            case DirectionalType.CounterClockwise:
                directionalConstant = 1;
                currentAngle = -differenceAngle;
                break;
            case DirectionalType.Clockwise:
            case DirectionalType.PingPongSingleEdge:
            case DirectionalType.PingPongDoubleEdge:
                directionalConstant = -1;
                currentAngle = differenceAngle;
                break;
        }
    }
    private void UpdateCurrentAngle()
    {
        currentAngle += angleInterval * directionalConstant;

        if (currentAngle < -differenceAngle || currentAngle > differenceAngle)
        {
            switch (directionalType)
            {
                // If sweep is clockwise, reset current angle 
                // back at the counter-clockwise-most position
                case DirectionalType.Clockwise:
                    currentAngle = differenceAngle;
                    break;
                // If sweep is counter-clockwise, reset current angle
                // back at the clockwise-most position
                case DirectionalType.CounterClockwise:
                    currentAngle = -differenceAngle;
                    break;
                // If sweep ping-pongs back and forth,
                // keep current angle and switch directions
                case DirectionalType.PingPongSingleEdge:
                case DirectionalType.PingPongDoubleEdge:
                    if (currentAngle < -differenceAngle)
                    {
                        directionalConstant = 1;
                    }
                    else
                    {
                        directionalConstant = -1;
                    }
                    // If ping ponging hits edges only once, move the current angle the other
                    // way twice - once past the overflow and once past the edge
                    if (directionalType == DirectionalType.PingPongSingleEdge)
                    {
                        currentAngle += angleInterval * directionalConstant * 2f;
                    }
                    // If ping ponging hits edges twice, move current angle 
                    // back across overflow to the edge
                    else
                    {
                        currentAngle += angleInterval * directionalConstant;
                    }
                    break;
            }
        }
    }

    // Provide the base vector. Determined by base class specifications
    protected abstract Vector2 BaseVector();
}
