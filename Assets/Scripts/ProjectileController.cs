using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{	
	[field: SerializeField]
	public float Speed { get; private set; }
	[field: SerializeField]
	public int Damage { get; private set; }
	
	private Vector2 Position { get; set; }
	private Vector2 Movement { get; set; }
	private Rigidbody2D Body { get; set; }

	private void Start()
	{
		Body = GetComponent<Rigidbody2D>();
	}

	public void Shoot(Vector2 direction)
	{
		Body.AddForce(direction, ForceMode2D.Impulse);
	}
}
