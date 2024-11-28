using System;
using UnityEngine;

public class UnitController : MonoBehaviour
{
	private HealthController _healthController;
	private MovementController _movementController;
	private VFXController _vfxController;
	protected void Awake()
	{
		_healthController = GetComponent<HealthController>();
		_movementController = GetComponent<MovementController>();
		_vfxController = GetComponent<VFXController>();
	}

	protected void Start()
	{
		_healthController.DeathEvent.AddListener(Kill);
		_healthController.DamageEvent.AddListener(Damage);
	}

	protected void Kill()
	{
		_movementController.Kill();
	}

	protected void Damage(int damage, Vector3 direction, float cooldown)
	{
		_vfxController.DamageVFX(direction, cooldown);
	}
}