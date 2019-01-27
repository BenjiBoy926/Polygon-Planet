using UnityEngine;
using System.Collections;

/*
 * CLASS EmitByInput2D
 * -------------------
 * A type of emitter that enables the user to emit the objects using input
 * emitting towards the mouse position
 * -------------------
 */ 

public class EmitByMouseInput2D : Emitter2D
{
    [SerializeField]
    private string emitButtonName;  // Name of the button in the input manager that the user presses to emit an object
    private Camera mainCamera;  // Stored reference to the main camera
    private Vector2 mousePosition;  // Position of the mouse in world space

    protected override void Start()
    {
        base.Start();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if(!emitted && Input.GetButton(emitButtonName))
        {
            if(mainCamera == null)
            {
                Debug.Log("Main Camera is null");
            }
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            ForceEmit(mousePosition - (Vector2)transform.position);
        }
    }
}
