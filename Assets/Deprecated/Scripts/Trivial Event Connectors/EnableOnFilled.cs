using UnityEngine;
using System.Collections;

// Enable the given game object when the given stockpile is filled
public class EnableOnFilled : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This object is enabled when the given stockpile is filled up")]
    private GameObject obj;
    [SerializeField]
    [Tooltip("Game object is enabled when this stockpile is filled up")]
    private Stockpile stock;

    private void Start()
    {
        stock.stockFilledEvent.AddListener(Enable);
    }

    private void Enable()
    {
        obj.SetActive(true);
    }
}
