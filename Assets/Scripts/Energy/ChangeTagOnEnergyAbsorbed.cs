using UnityEngine;
using System.Collections;

public class ChangeTagOnEnergyAbsorbed : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tag to change the energy socket to when it collides with the given energy source")]
    private Tag destinationTag;
    [SerializeField]
    [Tooltip("Energy socket that triggers the tag change " +
        "on the energy source that transfers the energy")]
    private EnergySocket socket;

    // Use this for initialization
    void Start()
    {
        socket.energyAbsorbedEvent.AddListener(x => ChangeTag(x.source));
    }

    private void ChangeTag(EnergySource source)
    {
        source.SetTag(destinationTag);
    }
}
