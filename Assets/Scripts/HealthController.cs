using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [field: SerializeField]
    private int MaxHealth { get; set; }
    private int Health { get; set; }

    private void Awake()
    {
        Health = MaxHealth;
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
