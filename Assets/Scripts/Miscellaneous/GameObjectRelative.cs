using UnityEngine;
using System.Collections.Generic;

public enum ParentOrChild { Parent, Child }

[System.Serializable]
public class GameObjectRelative
{
    [SerializeField]
    [Tooltip("Determine if the game object to delegate to" +
        " is a parent or child of this game object")]
    private ParentOrChild parentOrChild;

    [SerializeField]
    [Tooltip("The levels up the parent heirarchy that the " +
        "script should delegate to")]
    private int parentLevel;

    [SerializeField]
    [Tooltip("A list of ints to determine how the script will " +
        "follow the chain of this game object's children to get " +
        "the correct delegate game object")]
    private List<int> childIndeces;

    public GameObject gameObject
    {
        get
        {
            if (parentOrChild == ParentOrChild.Parent)
            {
                return GameObjectUtils.GetAncestor(gameObject, parentLevel);
            }
            else
            {
                return GameObjectUtils.GetDescendent(gameObject, childIndeces);
            }
        }
    }
}
