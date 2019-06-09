using UnityEngine;
using UnityEngine.Events;
using System;

/*
 * CLASS ParticleEffect
 * --------------------
 * Describes a single particle effect
 * Encapsulates useful functions that make activation/deactivation
 * of the particle effect quick and easy
 * --------------------
 */ 

public class ParticleEffect : MonoBehaviour, ILabelledComponent
{
    [SerializeField]
    [Tooltip("Public label describing the effect")]
    private string _label;
    public string label { get { return _label; } }
    [SerializeField]
    [Tooltip("Game object used as the particle effect")]
    private GameObject particlePrefab;
    [SerializeField]
    [Tooltip("Parent transform of the particle effect")]
    private Transform particleParent;
    private Transform particle; // Reference to the transform of the particle instantiated

    // Function that modifies the particle effect before enabling it
    public event Action<Transform> particleTransformer;

    private void Start()
    {
        if(particlePrefab != null)
        {
            // If a parent is specified, put the particle as a child
            if (particleParent != null)
            {
                particle = Instantiate(particlePrefab, particleParent).transform;
            }
            // Otherwise, put the particle without any parent
            else
            {
                particle = Instantiate(particlePrefab).transform;
            }
            particle.gameObject.SetActive(false);
        }
        else
        {
            particle = null;
        }
    }
    // Disable and enable the effect
    public void EnableEffect()
    {
        if(particle != null)
        {
            if(particleTransformer != null)
            {
                particleTransformer(particle);
            }
            particle.gameObject.SetActive(false);
            particle.gameObject.SetActive(true);
        }
    }
    // Disable the particle effect
    public void DisableEffect()
    {
        if (particle != null)
        {
            particle.gameObject.SetActive(false);
        }
    }
}