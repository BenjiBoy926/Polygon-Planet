using UnityEngine;
using UnityEditor;
using System;

/*
 * CLASS ParticleEffect
 * --------------------
 * Describes a single particle effect
 * Encapsulates useful functions that make activation/deactivation
 * of the particle effect quick and easy
 * --------------------
 */ 

[Serializable]
public class ParticleEffect
{
    [SerializeField]
    private GameObject particlePrefab;  // Game object used as the particle effect
    [SerializeField]
    private Transform particleParent;   // Parent transform of the particle effect
    private Transform particle; // Reference to the transform of the particle instantiated

    public void Initialize()
    {
        // If a parent is specified, put the particle as a child
        if (particleParent != null)
        {
            particle = UnityEngine.Object.Instantiate(particle, particleParent).transform;
        }
        // Otherwise, put the particle without any parent
        else
        {
            particle = UnityEngine.Object.Instantiate(particle).transform;
        }
        particle.gameObject.SetActive(false);
    }
    // Disable and enable the effect
    public void EnableEffect()
    {
        particle.gameObject.SetActive(false);
        particle.gameObject.SetActive(true);
    }
    // Enable the effect at the given position, either in world space or local space
    public void EnableEffect(Vector3 position, Space transformSpace = Space.World)
    {
        if(transformSpace == Space.World)
        {
            particle.position = position;
        }
        else
        {
            particle.localPosition = position;
        }
        EnableEffect();
    }
    // Disable the particle effect
    public void DisableEffect()
    {
        particle.gameObject.SetActive(false);
    }
}