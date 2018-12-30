using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Previewer : MonoBehaviour
{
    private Camera mainCamera;  // Reference to the main camera
    [SerializeField]
    private Camera previewCamera;   // Prefab of the camera used to preview the stage
    [SerializeField]
    private float previewTime;
    [SerializeField]
    private List<string> inputButtons;  // If any button with this name is pressed, the preview ends and the game begins

    private void Start()
    {
        previewCamera = Instantiate(previewCamera);
        mainCamera = Camera.main;

        StopAllCoroutines();
        StartCoroutine("PreviewScene");
    }

    private IEnumerator PreviewScene()
    {
        // Set the preview camera
        mainCamera.enabled = false;
        previewCamera.enabled = true;
        Timekeeper.instance.PauseGame(true);

        yield return new WaitForSecondsRealtime(previewTime);

        // Probably bring up some kind of prompt that the Player is free to move now
        Debug.Log("Scene ready to start!");

        yield return new WaitUntil(InputButtonPressed);

        // Unpause the game and enable the main camera
        mainCamera.enabled = true;
        previewCamera.enabled = false;
        Timekeeper.instance.PauseGame(false);
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
