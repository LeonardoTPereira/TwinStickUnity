using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [field: SerializeField]
    private int MaxHealth { get; set; }

    public UnityEvent DeathEvent { get; private set; }
    private int Health { get; set; }

    private void Awake()
    {
        Health = MaxHealth;
        DeathEvent = new UnityEvent();
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health > 0) return;
        DeathEvent.Invoke();
        Destroy(gameObject);
    }
}
