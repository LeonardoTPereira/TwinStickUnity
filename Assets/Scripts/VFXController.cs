using System;
using System.Collections;
using UnityEngine;

public class VFXController: MonoBehaviour
{
	[field: SerializeField]
	public ParticleSystem BloodParticle { get; private set; }

	private SpriteRenderer _spriteRenderer;
	protected void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void DamageVFX(Vector3 impactDirection, float cooldown)
	{
		StartCoroutine(FlashColors(cooldown));
		//EmitBloodParticle(impactDirection);
	}

	private IEnumerator FlashColors(float cooldown)
	{
		_spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(cooldown);
		_spriteRenderer.color = Color.white;
	}

	private void EmitBloodParticle(Vector3 impactDirection)
	{
		var mainParticle= BloodParticle.main;
		mainParticle.startSpeed = 0;
		var forceOverLifetime = BloodParticle.forceOverLifetime;
		forceOverLifetime.enabled = true;
		forceOverLifetime.x = impactDirection.x * 40;
		forceOverLifetime.y = impactDirection.y * 40;
		forceOverLifetime.z = impactDirection.z * 40;
		BloodParticle.Play();
	}
}