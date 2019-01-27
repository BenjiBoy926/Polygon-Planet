using UnityEngine;
using System.Collections;

/*
 * CLASS ParticleEffector : MonoBehaviour
 * --------------------------------------
 * Simple script is given a game object that functions as a particle effect,
 * instantiates it, and can activate and deactivate it at the given position
 * --------------------------------------
 */ 

public class ParticleEffector : MonoBehaviour
{
    [SerializeField]
    private GameObject particle;    // Reference to the object prefab instantiated and used as the particle effect
    [SerializeField]
    private Transform particleParent;   // Reference to a transform that the particle is instantiated as a child of
    private Transform particleTransform;    // Reference to a copy of the prefab instantiated

    protected virtual void Start()
    {
        // If a parent is specified, put the particle as a child
        if(particleParent != null)
        {
            particleTransform = Instantiate(particle, particleParent).transform;
        }
        // Otherwise, put the particle without any parent
        else
        {
            particleTransform = Instantiate(particle).transform;
        }

        particleTransform.gameObject.SetActive(false);
    }

    // Re-enable the particle effect
    public virtual void EnableEffect()
    {
        particleTransform.gameObject.SetActive(false);
        particleTransform.gameObject.SetActive(true);
    }
    // Move the particle effect to the specified global position and (re)activate it
    public virtual void EnableEffect(Vector3 position)
    {
        particleTransform.position = position;
        EnableEffect();
    }
    // Move the particle effect to the local position and activate it
    public virtual void EnableEffectLocal(Vector3 localPos)
    {
        particleTransform.localPosition = localPos;
        EnableEffect();
    }
    // Disable the particle effect
    public virtual void DisableEffect()
    {
        particleTransform.gameObject.SetActive(false);
    }
}
