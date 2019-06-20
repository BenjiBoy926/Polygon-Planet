using UnityEngine;
using System;

[Serializable]
public class VectorToTarget2D
{
    [SerializeField]
    [Tooltip("Transform component of the object to point away from")]
    private Transform start;
    [SerializeField]
    [Tooltip("Tag of the object to point to with the target vector")]
    private string targetTag;
    private Transform target;

    public void Initialize()
    {
        GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);

        if (targetObject != null)
        {
            target = targetObject.transform;
        }
        else
        {
            throw new NullReferenceException("No game object with tag " + targetTag + " was found");
        }
    }

    public bool Initialized()
    {
        return target != null;
    }

    public static implicit operator Vector2(VectorToTarget2D toTarget2D)
    {
        if (!toTarget2D.Initialized())
        {
            toTarget2D.Initialize();
        }
        return toTarget2D.target.position - toTarget2D.start.position;
    }
}
