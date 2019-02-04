using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS BackupCamera
 * ------------------
 * Simple script backs up the basic default camera by activating 
 * a backup camera if the default camera is disabled
 * ------------------
 */ 

public class BackupCamera : MonoBehaviour
{
    [SerializeField]
    private string defaultCameraTag;
    [SerializeField]
    private string backupCameraTag;

    private GameObject defaultCamera;
    private GameObject backupCamera;

    private void Start()
    {
        defaultCamera = GameObject.FindGameObjectWithTag(defaultCameraTag);
        backupCamera = GameObject.FindGameObjectWithTag(backupCameraTag);
        backupCamera.SetActive(false);
    }

    private void Update()
    {
        if(!defaultCamera.activeInHierarchy && !backupCamera.activeInHierarchy)
        {
            backupCamera.SetActive(true);
        }
    }
}
