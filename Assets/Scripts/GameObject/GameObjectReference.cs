using UnityEngine;
using System.Collections.Generic;

public class GameObjectReference : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How the component in question is going to be references")]
    private ReferenceType referenceType;

    [SerializeField]
    [Tooltip("The GameObject that is expected to have the desired component")]
    private GameObject gObject;

    [SerializeField]
    [Tooltip("The tag of the GameObject to refer to")]
    private string gObjectTag;

    [SerializeField]
    [Tooltip("Determine if the related game object is a parent of this game object or one of it's children")]
    private ParentOrChild parentOrChild;

    [SerializeField]
    [Tooltip("Level up the heirarchy to look for the parent")]
    private int parentLevel;

    [SerializeField]
    [Tooltip("Indeces of the children to trace through to the desired child")]
    private List<int> childIndeces;

    public GameObject gameObjectReferenced
    {
        get
        {
            switch(referenceType)
            {
                case ReferenceType.Self: return gameObject;
                case ReferenceType.DropTarget: return gObject;
                case ReferenceType.ByTag: return GameObject.FindGameObjectWithTag(gObjectTag);
                default: return gameObject.transform.GetRelative(parentOrChild, parentLevel, childIndeces).gameObject;
            }
        }
    }
}

public enum ReferenceType
{
    Self, DropTarget, ByTag, ByRelative
}
