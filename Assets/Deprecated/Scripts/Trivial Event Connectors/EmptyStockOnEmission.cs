using UnityEngine;

public class EmptyStockOnEmission : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Game object that has an emitter component on it")]
    private EmitterComponent emitter;
    [SerializeField]
    [Tooltip("Stock to empty out when the given emitter emits")]
    private Stockpile stockpile;

    // Use this for initialization
    void Start()
    {
        //emitter.Setup();
        //emitter.script.emissionEvent.AddListener(x => stockpile.EmptyStock()); 
    }
}
