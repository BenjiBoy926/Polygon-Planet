using UnityEngine;

// Simple attribute that disables any energy source it comes into contact with
public class EnergySourceNullifier : MonoBehaviour
{
    public event UnityAction<EnergyNullifiedEventData> energyNullifiedEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnergySource source = collision.GetComponent<EnergySource>();
        if(source != null)
        {
            Nullify(source);
        }
    }

    private void Nullify(EnergySource source)
    {
        source.gameObject.SetActive(false);
        if(energyNullifiedEvent != null)
        {
            energyNullifiedEvent(new EnergyNullifiedEventData(this, source));
        }
    }
}
