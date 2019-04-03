using UnityEngine;

// Simple attribute that disables any energy source it comes into contact with
public class EnergySourceNullifyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnergySource source = collision.GetComponent<EnergySource>();
        if(source != null)
        {
            collision.gameObject.SetActive(false);
        }
    }
}
