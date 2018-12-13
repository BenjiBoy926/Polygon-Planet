using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Emitter2D
{
    [SerializeField]
    private float _moveSpeedMultiplier; // Player move speed is slowed by this constant while the gun is firing
    public float moveSpeedMultiplier { get { return _moveSpeedMultiplier; } }
}
