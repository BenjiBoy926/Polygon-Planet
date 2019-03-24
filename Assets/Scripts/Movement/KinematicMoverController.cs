using UnityEngine;
using System.Collections;

/*
 * CLASS KinematicMoverController
 * ------------------------------
 * Base class for any script that intends to take control
 * of a kinematic mover
 * ------------------------------
 */ 

public class KinematicMoverController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Script the controller uses to move the object around")]
    protected KinematicMover2D mover;
    [SerializeField]
    [Tooltip("Speed at which the controller moves the object around")]
    protected float _speed;
    // Make speed variable public to all
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
}
