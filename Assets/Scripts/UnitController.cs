using System;
using UnityEngine;

public class UnitController: MonoBehaviour
{
	private HealthController _healthController;
	private MovementController _movementController;
	protected void Awake()
	{
		_healthController = GetComponent<HealthController>();
		_movementController = GetComponent<MovementController>();
	}

	protected void Start()
	{
		_healthController.DeathEvent.AddListener(Kill);
	}

	protected void Kill()
	{
		_movementController.Kill();
	}
}