using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[field: SerializeField]
	public float Speed { get; private set; }
	public Vector2 Movement { get; private set; }
	public Rigidbody2D Body { get; private set; }

	private void Awake()
	{
		Movement = Vector2.zero;
	}

	private void Start()
	{
		Body = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Body.velocity = Movement * Speed;
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		if (context.canceled)
		{
			Movement = Vector2.zero;
			return;
		}
		if (!context.performed) return;
		var movement = context.ReadValue<Vector2>();
		movement.Normalize();
		Movement = movement * Speed;
		UpdateRotation(movement);
	}

	private void UpdateRotation(Vector2 movement)
	{
		var zAngle = 0;
		if (movement.x > 0)
		{
			zAngle = 90;
		}
		else if (movement.x < 0)
		{
			zAngle = -90;
		}
		if (Mathf.Abs(movement.x) - Mathf.Abs(movement.y) > 0)
		{
			transform.eulerAngles = new Vector3(0, 0, zAngle);
			return;
		}
		if (movement.y > 0)
		{
			zAngle = 180;
		}
		else if (movement.y < 0)
		{
			zAngle = 0;
		}
		transform.eulerAngles = new Vector3(0, 0, zAngle);
	}
}
