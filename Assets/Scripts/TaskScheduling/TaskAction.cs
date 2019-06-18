using UnityEngine;
using System;

[Serializable]
public class TaskAction : Task
{
    /*
     * PUBLIC TYPEDEFS
     */

    [Serializable]
    public class RunnableComponent : PolymorphicComponent<IRunnable> { };

    /*
     * PUBLIC DATA
     */ 

    [SerializeField]
    [Tooltip("Reference to the script with the method that performs the task action")]
    private RunnableComponent invoker;

    public void Initialize()
    {
        invoker.Initialize();
    }

    public override void Run()
    {
        if (invoker.component == null)
        {
            Initialize();
        }
        invoker.component.Run();
    }
}
