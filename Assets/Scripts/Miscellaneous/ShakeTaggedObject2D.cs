using UnityEngine;

public class ShakeTaggedObject2D : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The tag of the object that will be shaken around")]
    private string objectTag;

    public float shakeMagnitude;
    public float shakeTime;
    public float shakeInterval;

    private GameObject _target;
    public GameObject target
    {
        get
        {
            if(_target == null)
            {
                _target = GameObject.FindGameObjectWithTag(objectTag);
                
                if (_target == null)
                {
                    throw new System.NullReferenceException("No game object find with tag " + objectTag);
                }
            }

            return _target;
        }
    }

    private Shaker _shaker;
    public Shaker shaker
    {
        get
        {
            if(_shaker == null)
            {
                _shaker = target.GetComponent<Shaker>();

                if (_shaker == null)
                {
                    throw new MissingComponentException("No component of type Shaker found on GameObject named " + target.name);
                }
            }

            return _shaker;
        }
    }

    public void Shake2D()
    {
        shaker.Shake2D(shakeMagnitude, shakeTime, shakeInterval);
    }
    public void Shake2DDefault()
    {
        shaker.Shake2D();
    }
}
