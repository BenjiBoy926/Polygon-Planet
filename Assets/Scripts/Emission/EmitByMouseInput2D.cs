using UnityEngine;
using System.Collections;

/*
 * CLASS EmitByMouseInput2D
 * ------------------------
 * Causes a constrained emitter to emit towards the mouse position
 * when the mouse button is pressed
 * ------------------------
 */

public class EmitByMouseInput2D : MonoBehaviour
{
    /*
     * FIELDS
     */ 

    [SerializeField]
    [Tooltip("Script used to produce the emission towards the mouse position")]
    private ConstrainedEmitter2D _emitter;
    [SerializeField]
    private string emitButtonName;  // Name of the button in the input manager that the user presses to emit an object
    [SerializeField]
    private InputButtonType buttonType;

    private Camera mainCamera;  // Stored reference to the main camera
    private Vector2 mousePosition;  // Position of the mouse in world space

    /*
     * METHODS
     */ 

    private void Start()
    { 
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        // If the emitter is ready to emit and input is currently queued, fire the emitter
        if(!_emitter.recentlyEmitted && InputExt.GetButton(emitButtonName, buttonType))
        {
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _emitter.ForceEmit(mousePosition - (Vector2)transform.position);
        }
    }
}
