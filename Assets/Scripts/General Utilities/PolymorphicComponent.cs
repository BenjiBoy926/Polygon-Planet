using UnityEngine;
using System;

[Serializable]
public class PolymorphicComponent<ComponentType>
{
    [SerializeField]
    [Tooltip("The game object that the given component will be attached to")]
    private GameObject obj;
    // The component attached to the game object given
    public ComponentType component { get; private set; }

    public void Initialize()
    {
        component = obj.GetComponent<ComponentType>();

        if (component == null)
        {
            throw new NullReferenceException("The game object with name " + obj.name + " must have a component of type " + component.GetType().ToString());
        }
    }

    public bool Initialized()
    {
        return component != null;
    }
}
