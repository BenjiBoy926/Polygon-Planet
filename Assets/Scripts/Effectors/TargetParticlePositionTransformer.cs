using UnityEngine;

public class TargetParticlePositionTransformer : ParticleEffectTransformer
{
    [SerializeField]
    [Tooltip("Transform component that the particle effect is moved to whenever it is enabled")]
    private Transform target;
    [SerializeField]
    [Tooltip("Offset from the target that the particle effect is moved to")]
    private Vector3 offset;

    protected override void ParticleTransformer(Transform particle)
    {
        particle.position = target.position + offset;
    }
}
