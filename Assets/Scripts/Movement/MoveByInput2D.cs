using UnityEngine;
using System.Collections;

/*
 * CLASS MoveByInput2D
 * -------------------
 * Enables a kinematic mover to be moved around using
 * configured inputs
 * -------------------
 */ 

public class MoveByInput2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the script that controls the movement of a kinemetic mover")]
    private KinematicMoverController controller;
    [SerializeField]
    private string horizontalButtonName;    // Name of the button in the input manager used to move sideways
    [SerializeField]
    private string verticalButtonName;  // Name of the button in the input manager used to move vertically
    private Vector2 moveVector = new Vector2(); // Vector used to move the object

    // Update is called once per frame
    void Update()
    {
        moveVector.x = Input.GetAxisRaw(horizontalButtonName);
        moveVector.y = Input.GetAxisRaw(verticalButtonName);
        controller.Move(moveVector);
    }
}
