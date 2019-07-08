using UnityEngine;
using System;

[Serializable]
public class PolymorphicComponent<ComponentType>
{
    [SerializeField]
    [Tooltip("The game object that the given component will be attached to")]
    private GameObject obj;

    // Reference to the component on this game object
    private ComponentType _component;

    // Component property
    public ComponentType component
    {
        get
        {
            if (!Initialized())
            {
                Initialize();
            }
            return _component;
        }
    }

    public void Initialize()
    {
        _component = obj.GetComponent<ComponentType>();

        if (component == null)
        {
            throw new NullReferenceException("The game object with name " + obj.name + " must have a component of type " + component.GetType().ToString());
        }
    }

    public bool Initialized()
    {
        return _component != null;
    }
}
