using UnityEngine;

[System.Serializable]
public class EmitterComponent
{
    [SerializeField]
    [Tooltip("Object with a script of type \"IEmitter\" attached as a component")]
    private GameObject emitterObject;
    public IEmitter script { get; private set; }

    public void Setup()
    {
        script = emitterObject.GetComponent<IEmitter>();

        if (script == null)
        {
            throw new System.NullReferenceException("The game object with name " + emitterObject.name + " must have a component of type \"IEmitter\"");
        }
    }
}
