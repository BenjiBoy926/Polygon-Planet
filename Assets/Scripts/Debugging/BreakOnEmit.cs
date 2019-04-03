using UnityEngine;
using System.Collections;

// Pause the game view when the given emitter emits
public class BreakOnEmit : MonoBehaviour
{
    [SerializeField]
    private Emitter2D emitter;
    private void Start()
    {
        emitter.emittedEvent += Break;
    }
    private void Break(Vector2 emissionDir)
    {
        Debug.Break();
    }
}
