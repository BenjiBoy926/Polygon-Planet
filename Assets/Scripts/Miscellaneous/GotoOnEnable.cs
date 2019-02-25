using UnityEngine;
using System.Collections;

/*
 * CLASS GotoOnEnable
 * ------------------
 * Move this object to the transform position of the desired 
 * with the desired offset
 * ------------------
 */ 

public class GotoOnEnable : MonoBehaviour
{
    [SerializeField]
    private string targetTag;   // Tag of the transform the object goes to
    [SerializeField]
    private Vector3 offset; // Offset to the target position

    private Transform target;   // Transform of the object to go to

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }
    private void OnEnable()
    {
        transform.position = target.position + offset;
    }
}
