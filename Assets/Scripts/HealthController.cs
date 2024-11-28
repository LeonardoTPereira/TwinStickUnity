using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [field: SerializeField]
    private int MaxHealth { get; set; }
    [field: SerializeField]
    private float InvincibilityCooldown { get; set; }
    public UnityEvent DeathEvent { get; private set; }
    public UnityEvent<int, Vector3, float> DamageEvent { get; private set; }
    private int Health { get; set; }

    private void Awake()
    {
        Health = MaxHealth;
        DamageEvent = new UnityEvent<int, Vector3, float>();
        DeathEvent = new UnityEvent();
    }

    public void Hit(int damage, Vector3 direction)
    {
        Health -= damage;
        DamageEvent.Invoke(damage, direction, InvincibilityCooldown);
        if (Health > 0) return;
        DeathEvent.Invoke();
        Destroy(gameObject);
    }
}
