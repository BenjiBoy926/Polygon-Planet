using UnityEngine;
using System.Collections.Generic;

public class LogarithmicDifficultyDriver : MonoBehaviour, IDifficultyDriver
{
    [SerializeField]
    [Tooltip("Tiers with objects that increase in difficulty")]
    private TierList tierList;
    [SerializeField]
    [Min(0)]
    [Tooltip("Interval ")]
    private int tierIntroductionInterval;

    public List<WeightedObject> Difficulty(int level)
    {
        return null;
    }
}
