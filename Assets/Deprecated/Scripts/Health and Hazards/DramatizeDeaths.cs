using UnityEngine;
using System.Collections.Generic;

/*
 * CLASS DramatizeDeaths
 * ---------------------
 * This script attempts to grab IDeathHandler scripts on all objects tagged
 * with the given tag. It causes slow motion and camera shake for a moment
 * after any of those death objects die
 * ---------------------
 */ 

public class DramatizeDeaths : DeathEventHandler
{
    [SerializeField]
    private string cameraTag;   // Tag of the camera that the script tries to shake
    [SerializeField]
    private float slowMotionScale;  // New timescale while scene is in slow motion
    [SerializeField]
    private float slowMotionTime;   // Time for which scene remains in slow motion
    private Shaker cameraShaker;    // Script used to shake the camera

    private void Start()
    {
        // Get a shaker script on the camera
        GameObject cameraObj = GameObject.FindGameObjectWithTag(cameraTag);
        cameraShaker = cameraObj.GetComponent<Shaker>();

        SubscribeToDeathEvents(DramatizeDeath);
    }

    // Go slow-motion for some time and shake the camera
    private void DramatizeDeath()
    {
        Timekeeper.instance.ScaledMoment(slowMotionScale, slowMotionTime);
        cameraShaker.Shake2D();
    }
}
