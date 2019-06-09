using UnityEngine;

public abstract class ParticleEffectTransformer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the particle effect to transform")]
    private ParticleEffect effect;

    // Use this for initialization
    void Start()
    {
        effect.particleTransformer += ParticleTransformer;
    }

    protected abstract void ParticleTransformer(Transform particle);
}
