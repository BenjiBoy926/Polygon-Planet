using UnityEngine;
using System;

public class Vector2Task : Task
{
    /*
     * PUBLIC TYPEDEFS
     */ 
    [Serializable]
    public class ConsumerComponent : PolymorphicComponent<IConsumer<Vector2>> { };
    [Serializable]
    public class SupplierComponent : PolymorphicComponent<ISupplier<Vector2>> { };

    [SerializeField]
    [Tooltip("Object that consumes the supplied Vector2 each time the task is invoked")]
    private ConsumerComponent consumer;
    [SerializeField]
    [Tooltip("Object that supplies the Vector2 each time the task is invoked")]
    private SupplierComponent supplier;

    public void Initialize()
    {
        consumer.Initialize();
        supplier.Initialize();
    }

    public override void Run()
    {
        if (!consumer.Initialized() || !supplier.Initialized())
        {
            Initialize();
        }
        consumer.component.Consume(supplier.component.Supply());
    }
}
