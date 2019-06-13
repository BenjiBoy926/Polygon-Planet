using UnityEngine;
using System.Collections;

public class ScaleSpeedOnObjectDisabled : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the script that controls movement of some object")]
    private KinematicMoverController controller;
    [SerializeField]
    [Tooltip("Script that raises events when the object is enabled and disabled")]
    private MonoBehaviourEvents objectEvents;
    [SerializeField]
    [Tooltip("Scalar applied to the speed while the object is disabled")]
    private float objectDisabledSpeedScalar;
    private bool scalarApplied;

    // Use this for initialization
    void Start()
    {
        scalarApplied = false;
        objectEvents.onEnable.AddListener(ObjectEnabledSpeed);
        objectEvents.onDisable.AddListener(ObjectDisabledSpeed);
    }

    // Apply speed scalar while the object is disabled
    private void ObjectDisabledSpeed()
    {
        if(!scalarApplied)
        {
            controller.AddSpeedScalar(objectDisabledSpeedScalar);
            scalarApplied = true;
        }
    }
    // Apply speed scalar while the object is enabled
    private void ObjectEnabledSpeed()
    {
        if(scalarApplied)
        {
            controller.RemoveSpeedScalar(objectDisabledSpeedScalar);
            scalarApplied = false;
        }
    }
}
