using UnityEngine;
using UnityEngine.Events;

public class VariableComponent<T> : MonoBehaviour, ILabelledComponent
{
    [SerializeField]
    [Tooltip("String label to represent this variable")]
    private string _label;
    public string label { get { return _label; } }
}
