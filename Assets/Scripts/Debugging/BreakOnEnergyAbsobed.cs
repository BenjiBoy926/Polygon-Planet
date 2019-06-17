using UnityEngine;
using System.Collections;

public class BreakOnEnergyAbsobed : MonoBehaviour
{
    [SerializeField]
    private EnergySocket socket;
    // Use this for initialization
    void Start()
    {
        //socket.energyAbsorbedEvent.AddListener(Break);
    }

    // Update is called once per frame
    void Break(EnergyAbsorbedEventData eventData)
    {
        Debug.Break();
    }
}
