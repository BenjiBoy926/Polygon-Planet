using System.Collections.Generic;
using UnityEngine;

/*
 * CLASS StockpileObjectUI
 * -----------------------
 * An object-based UI that instantiates an object for each stock in the stockpile
 * then enables/disables them as the stock changes
 * -----------------------
 */ 

public class StockpileObjectUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Stockpile with associated UI")]
    private Stockpile stockpile;
    [SerializeField]
    [Tooltip("Object that indicates each individual stock")]
    private GameObject indicatorPrefab;
    [SerializeField]
    [Tooltip("Transform that all indicators are instantiated as children under")]
    private Transform parent;

    // List of indicator prefabs instantiated for each stock
    private List<GameObject> indicators = new List<GameObject>();

    private void Start()
    {
        SetupIndicators();
        RefreshIndicators();

        stockpile.stockChangedEvent += (arg) => RefreshIndicators();
    }

    private void SetupIndicators()
    {
        for(int i = 0; i < stockpile.maxStock; i++)
        {
            indicators.Add(Instantiate(indicatorPrefab, parent));
        }
    }

    private void RefreshIndicators()
    {
        for(int i = 0; i < stockpile.maxStock; i++)
        {
            if(i < stockpile.currentStock)
            {
                indicators[i].SetActive(true);
            }
            else
            {
                indicators[i].SetActive(false);
            }
        }
    }
}
