using UnityEngine;
using System.Collections;

/*
 * CLASS RotateToMouse2D
 * ---------------------
 * The object rotates towards the mouse on a 2D plane
 * ---------------------
 */ 

public class RotateToMouse2D : MonoBehaviour
{
    [SerializeField]
    private Vector2 objectForward;  // Definition of the object's "forward" direction
    // Vector with the tail at this object and the tip at mouse position
    private Vector2 toMouse = new Vector2();
    // Position of the mouse in world coordinates
    private Vector2 mousePosition;
    // Camera that the mouse's position is measured from
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        toMouse = mousePosition - (Vector2)transform.position;
        transform.LookInDirection2D(toMouse, objectForward);
    }
}
