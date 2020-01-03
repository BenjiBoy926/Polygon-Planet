using UnityEngine;
using System.Collections;

public class VariableComponentReference<T> : MonoBehaviour where T : VariableComponent<T>
{
    [SerializeField]
    [Tooltip("Used to identify the variable component referenced")]
    private LabelledComponentID variableComponentID;

}