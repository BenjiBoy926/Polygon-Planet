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
    [SerializeField]
    [Tooltip("Script used to produce the emission towards the mouse position")]
    private ConstrainedEmitter2D emitter;
    [SerializeField]
    private string emitButtonName;  // Name of the button in the input manager that the user presses to emit an object
    private Camera mainCamera;  // Stored reference to the main camera
    private Vector2 mousePosition;  // Position of the mouse in world space
    private void Start()
    { 
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update()
    {
        if(!emitter.recentlyEmitted && Input.GetButton(emitButtonName))
        {
            if(mainCamera == null)
            {
                Debug.Log("Main Camera is null");
            }
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            emitter.ForceEmit(mousePosition - (Vector2)transform.position);
        }
    }
}
