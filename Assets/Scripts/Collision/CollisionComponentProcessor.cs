using UnityEngine;

/*
 * CLASS CollisionComponentProcessor
 * ---------------------------------
 * Base class for any object that tries to get a component off of
 * any object it collides with and process the component grabbed
 * ---------------------------------
 */ 
public abstract class CollisionComponentProcessor<TComponent> : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        TryProcessComponent(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryProcessComponent(collision.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        TryProcessComponent(other.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryProcessComponent(collision.gameObject);
    }

    private void TryProcessComponent(GameObject obj)
    {
        TComponent component = obj.GetComponent<TComponent>();
        if (component != null)
        {
            try
            {
                ProcessComponent(component);
            }
            catch (MissingComponentException) { }
        }
    }

    protected abstract void ProcessComponent(TComponent component);
}
