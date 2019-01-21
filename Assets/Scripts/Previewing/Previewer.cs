using UnityEngine;
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
    private Camera mainCamera;  // Reference to the main camera
    [SerializeField]
    private Camera previewCamera;   // Reference to the camera used to preview the scene
    [SerializeField]
    private float previewTime;
    [SerializeField]
    private List<string> inputButtons;  // If any button with this name is pressed, the preview ends and the game begins

    protected virtual void Start()
    {
        mainCamera = Camera.main;
    }

    public void StartPreview()
    {
        // Set the preview camera
        mainCamera.enabled = false;
        previewCamera.enabled = true;
        Timekeeper.instance.PauseGame(true);

        StopAllCoroutines();
        StartCoroutine("PreviewScene");
    }

    public void StopPreview()
    {
        StopAllCoroutines();

        // Unpause the game and enable the main camera
        mainCamera.enabled = true;
        previewCamera.enabled = false;
        Timekeeper.instance.PauseGame(false);
    }

    private IEnumerator PreviewScene()
    {
        yield return new WaitForSecondsRealtime(previewTime);

        // Probably bring up some kind of prompt that the Player is free to move now
        Debug.Log("Scene ready to start!");

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
