using UnityEngine.Events;

public interface IDeathHandler
{
    void Die();
    void AddDeathEvent(UnityAction method);
    void RemoveDeathEvent(UnityAction method);
}