using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * CLASS LoadSceneOnAllKilled
 * --------------------------
 * A type of death event handler that loads the scene with the
 * specified name once all of the death event objects are dead
 * --------------------------
 */ 

public class LoadSceneOnAllKilled : DeathEventHandler
{
    [SerializeField]
    private string sceneName;   // Name of the scene loaded when all are killed
    [SerializeField]
    private float loadDelay;    // Delay between death of all objects and loading the next level 

    // Start is called before the first frame update
    void Start()
    {
        SubscribeToDeathEvents(CheckDeathHandles);
    }

    private void CheckDeathHandles()
    {
        // If all death handles are dead, load level after delay time
        if(deathHandles.TrueForAll(x => x.isDead))
        {
            CancelInvoke();
            Invoke("LoadLevel", loadDelay);
        }
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);   
    }
}
