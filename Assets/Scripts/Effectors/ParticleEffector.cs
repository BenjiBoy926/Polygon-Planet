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
    private GameObject particlePrefab;    // Reference to the object prefab instantiated and used as the particle effect
    private Transform particle;    // Reference to a copy of the prefab instantiated

    protected virtual void Start()
    {
        particle = Instantiate(particlePrefab).transform;
        particle.gameObject.SetActive(false);
    }

    // Move the particle effect to the specified global position and (re)activate it
    public virtual void EnableEffect(Vector3 position)
    {
        particle.position = position;
        particle.gameObject.SetActive(false);
        particle.gameObject.SetActive(true);
    }
    // Disable the particle effect
    public virtual void DisableEffect()
    {
        particle.gameObject.SetActive(false);
    }
}
