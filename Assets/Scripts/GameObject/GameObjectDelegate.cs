using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDelegate : MonoBehaviour
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

    // The GameObject referenced with a position in the heirarchy relative to this game object
    private GameObject _delegateObject;
    public GameObject delegateObject
    {
        get
        {
            if (_delegateObject == null)
            {
                if (parentOrChild == ParentOrChild.Parent)
                {
                    _delegateObject = GameObjectUtils.GetAncestor(gameObject, parentLevel);
                }
                else
                {
                    _delegateObject = GameObjectUtils.GetDescendent(gameObject, childIndeces);
                }
            }
            return _delegateObject;
        }
    }

    public void SetActive(bool value)
    {
        delegateObject.SetActive(value);
    }
}
