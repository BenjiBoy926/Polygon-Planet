using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneManagerBehaviour : MonoBehaviour
{
    [System.Serializable]
    public class AsyncOperationEvent : UnityEvent<AsyncOperation> { };

    [SerializeField]
    [Tooltip("Event invoked when a scene is loaded with the async operation")]
    private AsyncOperationEvent _sceneAsyncLoaded;
    public AsyncOperationEvent sceneAsyncLoaded { get { return _sceneAsyncLoaded; } }

    // LoadScene
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // LoadSceneAsync
    public void LoadSceneAsync(int buildIndex)
    {
        _sceneAsyncLoaded.Invoke(SceneManager.LoadSceneAsync(buildIndex));
    }
    public void LoadSceneAsync(string sceneName)
    {
        _sceneAsyncLoaded.Invoke(SceneManager.LoadSceneAsync(sceneName));
    }

    // ReloadScene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReloadSceneAsync()
    {
        _sceneAsyncLoaded.Invoke(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name));
    }

    // LoadNextScene
    public void LoadNextScene(string sceneNamePrefix)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string currentLevelString = currentScene.Substring(sceneNamePrefix.Length, currentScene.Length - sceneNamePrefix.Length);
        int currentLevel = -1;

        int.TryParse(currentLevelString, out currentLevel);

        if (currentLevel >= 1)
        {
            SceneManager.LoadScene(sceneNamePrefix + (currentLevel + 1));
        }
        else
        {
            Debug.LogError("The current level of the current scene named " + currentScene + " could not be determined");
        }
    }
    public void LoadNextSceneAsync(string sceneNamePrefix)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string currentLevelString = currentScene.Substring(sceneNamePrefix.Length, currentScene.Length - sceneNamePrefix.Length);
        int currentLevel = -1;

        int.TryParse(currentLevelString, out currentLevel);

        if (currentLevel >= 1)
        {
            _sceneAsyncLoaded.Invoke(SceneManager.LoadSceneAsync(sceneNamePrefix + (currentLevel + 1)));
        }
        else
        {
            Debug.LogError("The current level of the current scene named " + currentScene + " could not be determined");
        }
    }
}
