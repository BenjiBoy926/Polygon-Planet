using UnityEngine;

public class AsyncSceneLoadSlider : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Reference to the scrip that raises the event when " +
        "a scene is loaded asynchronously. The asynchronous loading " +
        "of the scene must be triggered through this script in order " +
        "to display the scene loading slider")]
    private SceneManagerBehaviour sceneManager;
    [SerializeField]
    [Tooltip("The script that will manage the display of the slider " +
        "when the async scene loader is triggered")]
    private AsyncSlider slider;

    private void Awake()
    {
        sceneManager.sceneAsyncLoaded.AddListener(StartAsyncSlider);
    }
    private void StartAsyncSlider(AsyncOperation operation)
    {
        slider.StartAsyncSlider(operation);
    }
}
