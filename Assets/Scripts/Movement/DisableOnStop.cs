using UnityEngine;
using System.Collections;

// Disable the given game object when the given kinematic mover is stopped
public class DisableOnStop : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Object that will be disabled when the given kinematic mover is stopped")]
    private GameObject obj;
    [SerializeField]
    [Tooltip("Object is disabled when this mover is stopped")]
    private KinematicMover2D mover;

    private void Start()
    {
        mover.stopEvent += Disable;
    }

    private void Disable()
    {
        obj.SetActive(false);
    }
}
