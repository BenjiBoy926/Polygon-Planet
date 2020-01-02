using System.Collections.Generic;
using UnityEngine;

public class DifficultyInterpreter<T> : MonoBehaviour where T : Object
{
    [SerializeField]
    [Tooltip("A GameObject which is assumed to have a component " +
        "of type IDifficultyDriver")]
    private GameObject difficultyDriverObject;

    private LazyLoader<IDifficultyDriver> difficultyDriver;

    private void Awake()
    {
        difficultyDriver = new LazyLoader<IDifficultyDriver>(() => difficultyDriverObject.GetComponent<IDifficultyDriver>());
    }

    public List<Pair<T, int>> DifficultyInterpretation(int level)
    {
        List<Pair<T, int>> interpretation = new List<Pair<T, int>>();
        List<WeightedObject> difficulty = difficultyDriver.obj.Difficulty(level);

        foreach(WeightedObject obj in difficulty)
        {
            interpretation.Add(new Pair<T, int>((T)obj.obj, obj.weight));
        }

        return interpretation;
    }
}
