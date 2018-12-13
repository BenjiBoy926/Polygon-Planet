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
    private Vector2 mousePosition;  // Position of the mouse in world space

    private void Update()
    {
        if(!primaryState && Input.GetButton(emitButtonName))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Emit(mousePosition - (Vector2)transform.position);
        }
    }
}
