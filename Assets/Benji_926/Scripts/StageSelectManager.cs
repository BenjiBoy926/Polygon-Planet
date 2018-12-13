using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField]
    private SceneSelectCreator creator; // Script that creates the buttons that load the levels

    void Awake()
    {
        creator.InitializeSceneSelectors(DataManager.Data.CompletedLevels);
    }
}
