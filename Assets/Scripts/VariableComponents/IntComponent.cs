using UnityEngine;
using UnityEngine.Events;

public class IntComponent : VariableComponent<int>
{
    [System.Serializable]
    public class TwoIntEvent : UnityEvent<int, int> { };

    [SerializeField]
    [Tooltip("The integer represented by this component")]
    private int _number;

    [SerializeField]
    private TwoIntEvent _onValueChanged;
    public TwoIntEvent onValueChanged { get { return _onValueChanged; } }

    public int number
    {
        get { return _number; }
        set
        {
            if(_number != value)
            {
                _onValueChanged.Invoke(_number, value);
                _number = value;
            }
        }
    }

    public static implicit operator int(IntComponent intComponent)
    {
        return intComponent.number;
    }

    public void Add(int add)
    {
        number += add;
    }
    public void Subtract(int sub)
    {
        number -= sub;
    }
    public void Multiply(int scalar)
    {
        number *= scalar;
    }
    public void Divide(int div)
    {
        number /= div;
    }
}
