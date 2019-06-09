using UnityEngine;

/*
 * CLASS DisableOnEmpty
 * --------------------
 * Disable the given object when the given stockpile goes empty
 * --------------------
 */ 

public class DisableOnEmpty : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private Stockpile stock;
    private void Start()
    {
        stock.stockEmptiedEvent.action += DisableObject;
    }
    private void DisableObject()
    {
        obj.SetActive(false);
    }
}