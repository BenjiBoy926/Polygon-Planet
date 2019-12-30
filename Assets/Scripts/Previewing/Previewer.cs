using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/*
 * CLASS Previewer : MonoBehaviour
 * -------------------------------
 * Creates a preivew of the scene by pausing the game
 * and switching to the preview camera, then switches
 * back and unpauses when the specified input buttons are pressed
 * -------------------------------
 */ 

public class Previewer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Camera that creates the preview")]
    private Camera previewCamera;   // Reference to the camera used to preview the scene
    [SerializeField]
    [Tooltip("Time for which the preview remains active before inputs can stop the preview")]
    private float previewTime;
    [SerializeField]
    [Tooltip("Names of the buttons in the input manager that " +
        "cause the preview to end")]
    private List<string> inputButtons;  // If any button with this name is pressed, the preview ends and the game begins

    [SerializeField]
    private UnityEvent _previewStartedEvent;
    public UnityEvent previewStartedEvent { get { return _previewStartedEvent; } }
    [SerializeField]
    private UnityEvent _previewUnlockedEvent;
    public UnityEvent previewUnlockedEvent { get { return _previewUnlockedEvent; } }
    [SerializeField]
    private UnityEvent _previewEndEvent;
    public UnityEvent previewEndEvent { get { return _previewEndEvent; } }

    private LazyLoader<Camera> mainCamera = new LazyLoader<Camera>(() => Camera.main);

    public void StartPreview()
    {
        _previewStartedEvent.Invoke();

        // Set the preview camera
        mainCamera.obj.enabled = false;
        previewCamera.enabled = true;
        Timekeeper.instance.PauseGame(true);

        StopAllCoroutines();
        StartCoroutine("PreviewScene");
    }

    public void StopPreview()
    {
        StopAllCoroutines();

        // Unpause the game and enable the main camera
        mainCamera.obj.enabled = true;
        previewCamera.enabled = false;
        Timekeeper.instance.PauseGame(false);

        _previewEndEvent.Invoke();
    }

    private IEnumerator PreviewScene()
    {
        yield return new WaitForSecondsRealtime(previewTime);

        _previewUnlockedEvent.Invoke();

        yield return new WaitUntil(InputButtonPressed);

        // Stop the preview once an input button is pressed
        StopPreview();
    }

    private bool InputButtonPressed()
    {
        bool buttonPressed = false;

        foreach(string button in inputButtons)
        {
            buttonPressed |= Input.GetButton(button);
        }

        return buttonPressed;
    }
}
