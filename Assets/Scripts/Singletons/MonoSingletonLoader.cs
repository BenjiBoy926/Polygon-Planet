﻿using UnityEngine;
using System.Collections;

/*
 * CLASS MonoSingletonLoader
 * -------------------------
 * This scripts loads all classes that derive from MonoSingleton
 * Highly specific to the needs of the current project, the MonoSingletonLoader
 * must be manually updated each time a MonoSingleton class is added
 * to the project.  It's annoying but it's the best solution I've got
 * -------------------------
 */ 

public class MonoSingletonLoader : MonoBehaviour
{
    private void Awake()
    {
        Timekeeper.CreateInstance(gameObject);
    }
}