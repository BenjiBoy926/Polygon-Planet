using UnityEngine;

public class DelayStockChangeOnEmission : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Script that changes the stock over time")]
    private ChangeStockOverTime stockChanger;
    [SerializeField]
    [Tooltip("Reference to the emitter that causes the stock change to delay")]
    private EmitterComponent emitter;
    [SerializeField]
    [Tooltip("Amount by which stock change is delayed each time the emitter emits")]
    private float delay;

    // Use this for initialization
    void Start()
    {
        //emitter.Setup();
        //emitter.script.emissionEvent.AddListener((arg) => stockChanger.DelayStockChange(delay));
    }
}
