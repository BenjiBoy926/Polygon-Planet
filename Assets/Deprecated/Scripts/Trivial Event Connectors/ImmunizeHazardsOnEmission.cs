using UnityEngine;

/*
 * CLASS ImmunizeHazardsOnEmission
 * -------------------------------
 * When the given emitter emits, the given socket is immunized from
 * hazards for the given amount of time
 * -------------------------------
 */ 

public class ImmunizeHazardsOnEmission : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Emitter that causes the socket to become immune to hazards when it emits")]
    private Emitter2D emitter;
    [SerializeField]
    [Tooltip("Socket that will be immunized from hazards when the emitter emits")]
    private EnergySocket socket;
    [SerializeField]
    [Tooltip("Time for which the socket is immunized from hazards whenever the emitter emits")]
    private float immunizationTime;

    private void Start()
    {
        emitter.emissionEvent.AddListener(ImmunizeOnEmit);
    }
    private void ImmunizeOnEmit(Vector2 emissionDir)
    {
        socket.ImmunizeHazards(immunizationTime);
    }
}
