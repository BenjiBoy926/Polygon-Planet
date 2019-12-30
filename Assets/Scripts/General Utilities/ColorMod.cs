using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// CLASS ColorMod
// Enables an image to smoothly transition between an
// "enabled" and "disabled" color
public class ColorMod : MonoBehaviour
{
    [SerializeField]
    private Image image;    // Image whose color will be modified
    [SerializeField]
    private Color enabledColor;  // Color when the image is "enabled"
    [SerializeField]
    private Color disabledColor;  // Color when the image is "disabled"
    [SerializeField]
    private float time; // Time it takes for the color transition to occur
    [SerializeField]
    [Tooltip("If true, the image color starts in the enabled color. " +
        "If false, the image color starts in the disabled color")]
    private bool startState;
    private float inverseTime;  // The inverse of time, stored for efficiency

    private void Awake ()
    {
        inverseTime = 1f / time;
    }

    private void OnEnable()
    {
        if(startState)
        {
            image.color = enabledColor;
        }
        else
        {
            image.color = disabledColor;
        }
    }

    // Use the correct coroutine to create the smooth transition between colors
    public void Transition (bool isEnabling)
    {
        if (isEnabling)
        {
            StartCoroutine("EnabledTrans");
        }
        else
        {
            StartCoroutine("DisabledTrans");
        }
    }

    // Smoothly transition the image's color from the
    // disabled to the enabled color
    private IEnumerator EnabledTrans ()
    {
        Vector4 diff;   // Difference vector between current and target color
        
        // Set image color and difference vector
        image.color = disabledColor;
        diff = image.color - enabledColor;

        // While difference between current and target colors is not negligible,
        // lerp image color towards the target color
        while (diff.sqrMagnitude > 0.01f)
        {
            image.color = Color.Lerp(image.color, enabledColor, Time.deltaTime * inverseTime);
            diff = image.color - enabledColor;
            yield return null;
        }
    }

    // Smoothly transition the image's color from the
    // enabled to the disabled color
    private IEnumerator DisabledTrans ()
    {
        Vector4 diff;   // Difference vector between current and target color
        
        // Set image color and difference vector
        image.color = enabledColor;
        diff = image.color - disabledColor;

        // While difference between current and target colors is not negligible,
        // lerp image color towards the target color
        while (diff.sqrMagnitude > 0.01f)
        {
            image.color = Color.Lerp(image.color, disabledColor, Time.deltaTime * inverseTime);
            diff = image.color - disabledColor;
            yield return null;
        }
    }
}
