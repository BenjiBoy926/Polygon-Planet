using UnityEngine;
using System.Collections;

// Enable the given game object when the given emitter emits
public class EnableOnEmit : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Game object that is enabled when the emitter emits")]
    private GameObject obj;
    [SerializeField]
    [Tooltip("Game object is enabled whenever this emitter emits")]
    private Emitter2D emitter;

    private void Start()
    {
        emitter.emittedEvent += Enable;
    }

    private void Enable(Vector2 emitDir)
    {
        obj.SetActive(true);
    }
}
