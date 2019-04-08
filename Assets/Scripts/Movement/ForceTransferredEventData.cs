using UnityEngine;
using System.Collections;

// Encapsulates data related to a force transfer between a force source and a fource socket
[System.Serializable]
public class ForceTransferredEventData
{
    [SerializeField]
    [Tooltip("The source that transferred the force")]
    private ForceSource _source;
    [SerializeField]
    [Tooltip("The socket that absorbed the force")]
    private ForceSocket _socket;
    [SerializeField]
    [Tooltip("Direction of the force")]
    private Vector2 _direction;
    [SerializeField]
    [Tooltip("Magnitude of the force transfer")]
    private float _strength;
    [SerializeField]
    [Tooltip("Time that the force lasts")]
    private float _time;

    public ForceSource source { get { return _source; } }
    public ForceSocket socket { get { return _socket; } }
    public Vector2 direction { get { return _direction; } }
    public float strength { get { return _strength; } }
    public float time { get { return _time; } }

    public ForceTransferredEventData(ForceSource forceSource, ForceSocket forceSocket, Vector2 dir, float stren, float t)
    {
        _source = forceSource;
        _socket = forceSocket;
        _direction = dir;
        _strength = stren;
        _time = t;
    }
}
