using UnityEngine;
using System.Collections;

public class BreakOnEnergyTransferred : MonoBehaviour
{
    [SerializeField]
    private EnergySource source;

    private void Start()
    {
        source.energyTransferredEvent.AddListener(Break);
    }

    private void Break(EnergyTransferredEventData eventData)
    {
        Debug.Break();
    }
}
