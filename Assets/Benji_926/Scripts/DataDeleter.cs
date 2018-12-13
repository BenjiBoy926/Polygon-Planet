using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDeleter : MonoBehaviour
{
    void Awake ()
    {
        DataManager.Delete();
    }
}
