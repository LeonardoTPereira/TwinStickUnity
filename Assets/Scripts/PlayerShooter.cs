using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
	[field: SerializeField]
	public float AttackSpeed { get; private set; }
	[field: SerializeField]
	private GameObject BulletSpawn { get; set; }
	[field: SerializeField]
	private GameObject BulletPrefab { get; set; }
	private float ProjectileSpeed { get; set; }
	private bool CanShoot { get; set; }
	private bool IsHoldingShoot { get; set; }
	private Rigidbody2D Body { get; set; }
	private const float MinShootMagnitude = 0.01f;

	private struct BulletForceAndRotation
	{
		public Vector2 Force;
		public int Rotation;
	}
	
	private void Awake()
	{
		CanShoot = true;
		Body = GetComponent<Rigidbody2D>();

	}
	
	public void OnShoot(InputAction.CallbackContext context)
	{
		if(context.performed)
		{
			IsHoldingShoot = true;
		}
		else if(context.canceled)
		{
			IsHoldingShoot = false;
		}

		var shootDirection = context.ReadValue<Vector2>();
		shootDirection.Normalize();
		if (shootDirection.magnitude > MinShootMagnitude)
			StartCoroutine(ShootBullet(shootDirection));
	}

	private IEnumerator ShootBullet(Vector2 shootDirection)
	{
		while (IsHoldingShoot)
		{
			yield return null;
			if (!CanShoot) continue;
			var bulletForceAndRotation = GetBulletForceAndRotation(shootDirection);
			RotateSpawnPoint(bulletForceAndRotation.Rotation);
			SpawnAndShootBullet(bulletForceAndRotation);
			var coolDownTime = GetCooldown();
			StartCoroutine(CountCooldown(coolDownTime));
		}
	}

	private BulletForceAndRotation GetBulletForceAndRotation(Vector2 projectileDirection)
	{
		BulletForceAndRotation bulletForceAndRotation;
		bulletForceAndRotation.Force = new Vector2(0, 1);
		bulletForceAndRotation.Rotation = 0;
		return bulletForceAndRotation;
	}

	private void RotateSpawnPoint(int rotation)
	{
		BulletSpawn.transform.rotation = Quaternion.Euler(0, 0, rotation);
	}

	private void SpawnAndShootBullet(BulletForceAndRotation bulletForceAndRotation)
	{
		var bullet = Instantiate(BulletPrefab, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
		var projectileController = bullet.GetComponent<ProjectileController>();
		projectileController.Shoot(bulletForceAndRotation.Force + Body.velocity.normalized);
	}

	private float GetCooldown()
	{
		return 1.0f / AttackSpeed;
	}

	private IEnumerator CountCooldown(float bulletCooldown)
	{
		CanShoot = false;
		yield return new WaitForSeconds(bulletCooldown);
		CanShoot = true;
	}
}
