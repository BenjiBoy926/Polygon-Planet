using UnityEngine;
using System.Collections;

/*
 * CLASS VolatileEnergyStockpileEffector
 * -------------------------------------
 * Produces a particle effect on each of the four energy stockpile events
 * It does not disable any particle effect, so the effects are expected
 * to disable themselves (are "volatile")
 * -------------------------------------
 */

public class VolatileEnergyStockpileEffector : MonoBehaviour
{
    [SerializeField]
    private Transform energyStockpile;  // Reference to the object that should have an energy stockpile script on it
    [SerializeField]
    private ParticleEffect energyIncreasedEffect;
    [SerializeField]
    private ParticleEffect energyDecreasedEffect;
    [SerializeField]
    private ParticleEffect energyFilledEffect;
    [SerializeField]
    private ParticleEffect energyEmptiedEffect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
