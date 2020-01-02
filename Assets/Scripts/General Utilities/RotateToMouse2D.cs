using UnityEngine;

/*
 * CLASS RotateToMouse2D
 * ---------------------
 * The object rotates towards the mouse on a 2D plane
 * ---------------------
 */ 

public class RotateToMouse2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the transform that will rotate")]
    private Transform trans;
    [SerializeField]
    private Vector2 objectForward;  // Definition of the object's "forward" direction
    // Vector with the tail at this object and the tip at mouse position
    private Vector2 toMouse = new Vector2();
    // Position of the mouse in world coordinates
    private Vector2 mousePosition;
    private LazyLoader<Camera> mainCamera = new LazyLoader<Camera>(() => Camera.main);

    protected virtual void Update()
    {
        if(mainCamera.obj != null)
        {
            mousePosition = mainCamera.obj.ScreenToWorldPoint(Input.mousePosition);
            toMouse = mousePosition - (Vector2)trans.position;
            trans.LookInDirection2D(toMouse, objectForward);
        }
    }
}
