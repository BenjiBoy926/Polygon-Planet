using UnityEngine;

public class SupplyFixedInt : MonoBehaviour
{
    [Tooltip("Fixed integer to supply to the consumer")]
    public int integer;

    public int Supply()
    {
        return integer;
    }
}
