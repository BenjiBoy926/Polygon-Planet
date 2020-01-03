using UnityEngine;

public class IntComponentReference : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The script that gets the int components")]
    private LabelledComponentKeeper intComponentReferences;

    public void Add(int add)
    {
        foreach(IntComponent integer in intComponentReferences.LabelledComponents<IntComponent>())
        {
            integer.Add(add);
        }
    }
    public void Subtract(int sub)
    {
        foreach (IntComponent integer in intComponentReferences.LabelledComponents<IntComponent>())
        {
            integer.Subtract(sub);
        }
    }
    public void Multiply(int scalar)
    {
        foreach (IntComponent integer in intComponentReferences.LabelledComponents<IntComponent>())
        {
            integer.Multiply(scalar);
        }
    }
    public void Divide(int div)
    {
        foreach (IntComponent integer in intComponentReferences.LabelledComponents<IntComponent>())
        {
            integer.Divide(div);
        }
    }
}
