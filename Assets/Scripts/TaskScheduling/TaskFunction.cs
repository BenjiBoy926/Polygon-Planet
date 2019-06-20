using UnityEngine;
using System;

public class TaskFunction<TResource> : Task
{
    [SerializeField]
    [Tooltip("A game object with a component that supplies the resource")]
    private GameObject supplier;
    private ISupplier<TResource> supplierScript;
    [SerializeField]
    [Tooltip("A game object with a component that consumes the resource")]
    private GameObject consumer;
    private IConsumer<TResource> consumerScript;

    private void Start()
    {
        supplierScript = supplier.GetComponent<ISupplier<TResource>>();
        consumerScript = consumer.GetComponent<IConsumer<TResource>>();

        if (supplierScript == null)
        {
            throw new NullReferenceException("Supplier script is null!");
        }
        if (consumerScript == null)
        {
            throw new NullReferenceException("Consumer script is null!");
        }
    }

    public override void Run()
    {
        consumerScript.Consume(supplierScript.Supply());
    }
}
