using UnityEngine;
using System.Collections;

/*
 * CLASS TargetingAutoEmitter2D
 * ----------------------------
 * A type of automatic emitter that tries to find a transform with the tag specified
 * and emits towards that transform
 * ----------------------------
 */ 

public class TargetingAutoEmitter2D : AutoEmitter2D
{
    [SerializeField]
    private string targetTag;   // Tag of the game object to target with the emitter
    private Transform targetTransform;  // Reference to the transform component on the targetted object

    protected override void Start()
    {
        base.Start();

        // Find the object with the tag and try to store the transform
        GameObject target = GameObject.FindGameObjectWithTag(targetTag);
        if(target != null)
        {
            targetTransform = target.transform;
            StartAutoEmitting(ToTarget);
        }
        else
        {
            Debug.Log(gameObject.name + " could not find any gameobject tagged " + targetTag);
        }
    }

    // Returns a vector with its tail at this object and the tip at the target
    private Vector2 ToTarget()
    {
        return targetTransform.position - transform.position;
    }
}
