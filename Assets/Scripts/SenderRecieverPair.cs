using UnityEngine;
using UnityEditor;

[System.Serializable]
public class SenderRecieverPair
{
    [SerializeField]
    private MonoBehaviour _sender;
    [SerializeField]
    private MonoBehaviour _receiver;

    public MonoBehaviour sender { get { return _sender; } }
    public MonoBehaviour receiver { get { return _receiver; } }

    public SenderRecieverPair (MonoBehaviour s, MonoBehaviour r)
    {
        _sender = s;
        _receiver = r;
    }
}