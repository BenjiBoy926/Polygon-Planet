using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreSelectManager : MonoBehaviour
{
    [SerializeField]
    private SceneSelectCreator creator; // Reference to the object that creates the lore selectors

    void Awake ()
    {
        creator.InitializeSceneSelectors(DataManager.Data.LoreViewed);
    }
}
