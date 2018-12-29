using UnityEngine;
using System.Collections;

/*
 * CLASS FreeMoveByInput2D
 * -----------------------
 * A type of 2-D blowback-able mover that can move freely (that is, without inhibition of gravity)
 * in a two dimensional space by user input
 * -----------------------
 */ 

public class MoveByInput2D : BlowbackMover2D
{
    [SerializeField]
    protected float _speed;  // Speed at which the object will move
    [SerializeField]
    private string horizontalButtonName;    // Name of the button in the input manager used to move sideways
    [SerializeField]
    private string verticalButtonName;  // Name of the button in the input manager used to move vertically
    private Vector2 moveVector = new Vector2(); // Vector used to move the object

    public float speed { set { _speed = value; } }

    // Update is called once per frame
    void Update()
    {
        moveVector.x = Input.GetAxisRaw(horizontalButtonName);
        moveVector.y = Input.GetAxisRaw(verticalButtonName);
        MoveTowards(moveVector, _speed);
    }
}
