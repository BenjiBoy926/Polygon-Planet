using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS DisableAfterTime
 * ----------------------
 * Simple script disables the object after a set amount of time
 * after it is enabled
 * ----------------------
 */ 

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField]
    private float time;

    private void OnEnable()
    {
        Invoke("Disable", time);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
